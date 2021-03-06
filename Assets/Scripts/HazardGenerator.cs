﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HazardGenerator : MonoBehaviour {
    // Yay magic numbers!
    private Vector3 TURTLE_BASE_COORDS = new Vector3(0, -2.6f, 0.25f);
    private Vector3 CROC_BASE_COORDS = new Vector3(0, -4.0f, 0.25f);
    private Vector3 HERON_BASE_COORDS = new Vector3(0, 0, 0.25f);

    private Quaternion BASE_ROTATION = new Quaternion(-0.7f, 0, 0, 0.7f);

    public Transform SpikedTurtle;
    public Transform Croc;
    public Transform Heron;

    private GameObject duck;

    private List<GameObject> activeHazards = new List<GameObject>();
    private const int ABSOLUTE_MAX_HAZARDS = 5;     // maxHazards cannot go above this
    private int maxHazards = 1;
    
    private const int HAZARD_RAMP_THRESHOLD_1 = 3;      // # of spawned enemies before we increase the number of enemies from 1 to 2
    private const int HAZARD_RAMP_THRESHOLD_2 = 10;     // # of spawned enemies before increase from 2 to 3
    private const int HAZARD_RAMP_THRESHOLD_3 = 15;     // 3 to 4
    private const int HAZARD_RAMP_THRESHOLD_4 = 30;     // 4 to 5

    private int[] hazardRampThresholds = new int[] {
        0,                          // This is here so we can directly index using maxHazards without an off-by-one correction
        HAZARD_RAMP_THRESHOLD_1,
        HAZARD_RAMP_THRESHOLD_2,
        HAZARD_RAMP_THRESHOLD_3,
        HAZARD_RAMP_THRESHOLD_4,
    };

    // Magic constant that is used in the spawn threshold adjustment formula below
    private float spawnAdjustConstant;

    private int totalSpawns = 0;
    private int spawnsSinceLastRamp = 0;
    
    private const float CROC_THRESHOLD_START = 0.0f;
    private const float CROC_THRESHOLD_END = 25.0f;

    private const float HERON_THRESHOLD_START = 0.0f;
    private const float HERON_THRESHOLD_END = 50.0f;
    
    private float crocThreshold = CROC_THRESHOLD_START;
    private float heronThreshold = HERON_THRESHOLD_START;

    private const int MIN_SPAWN_DIST = 25;
    private const int MAX_SPAWN_DIST = 75;

    // Minimum distance between spawned enemies.
    private const int MIN_ENEMY_DIST = 8;

    private const int CLEANUP_DIST = 10;

    // Use this for initialization
    void Start() {
        duck = GameObject.Find("Duck");

        int spawnThresholdTotal = 0;
        foreach (int spawnThreshold in hazardRampThresholds) {
            spawnThresholdTotal += spawnThreshold;
        }

        spawnAdjustConstant = ((float) spawnThresholdTotal) / 6.0f;
    }

    /* Randomly choose an enemy to spawn, with proper weighting. */
    private Transform choose_enemy_type(out Vector3 enemyBaseCoords) {
        float n = UnityEngine.Random.Range(0, 100);

        if (n < crocThreshold) {
            enemyBaseCoords = CROC_BASE_COORDS;
            return Croc;
        }
        else if (n < heronThreshold) {
            enemyBaseCoords = HERON_BASE_COORDS;
            return Heron;
        }
        else {
            enemyBaseCoords = TURTLE_BASE_COORDS;
            return SpikedTurtle;
        }

    }

    /* Are the two given enemies too close to each other? */
    private bool enemies_too_close(GameObject enemy1, GameObject enemy2) {
        float dist = enemy1.transform.position.x - enemy2.transform.position.x;
        return Math.Abs(dist) < MIN_ENEMY_DIST;
    }

    /* Spawn an enemy. Arrrh! */
    private void spawn_enemy() {
//        Debug.Log("Trying to spawn new enemy...");

        float distFromDuck;

        Transform newEnemyTransform;
        GameObject newEnemy = null;

        bool invalid = true;

        for (int i=0; i<10 && invalid; i++) {
            distFromDuck = UnityEngine.Random.Range(MIN_SPAWN_DIST, MAX_SPAWN_DIST);

            Vector3 enemyBaseCoords;
            Transform EnemyType = choose_enemy_type(out enemyBaseCoords);

            newEnemyTransform = (Transform) Instantiate(
                    EnemyType,
                    enemyBaseCoords + Vector3.right * (get_duck_pos() + distFromDuck),
                    BASE_ROTATION
                );

            newEnemy = newEnemyTransform.gameObject;

            invalid = false;

            foreach (GameObject enemy in activeHazards) {
                invalid = enemies_too_close(enemy, newEnemy);

                if (invalid) {
                    Destroy(newEnemy);  // Blah blah blah this is inefficient blah blah who cares
                    newEnemy = null;
                    break;
                }
            }

        }

        if (newEnemy != null) {
            activeHazards.Add(newEnemy);
//            Debug.Log("Spawned new enemy!");
        }
        
        // Otherwise something messed up
        System.Diagnostics.Debug.Assert(activeHazards.Count <= maxHazards);

    }

    /* Be a good citizen and clean up when you're finished. */
    private void clean_up_enemies() {
        List<GameObject> hazardsToDestroy = new List<GameObject>();

        // Build a list of hazards eligible for destruction
        foreach (GameObject hazard in activeHazards) {
            if (hazard.transform.position.x < get_duck_pos() - CLEANUP_DIST) {
                hazardsToDestroy.Add(hazard);
            }
        }

        // Destroy all eligible objects
        foreach (GameObject hazard in hazardsToDestroy) {
            activeHazards.Remove(hazard);
            Destroy(hazard);
        }

    }
    
    /* Get duck X position */
    private float get_duck_pos() {
        return duck.transform.position.x;
    }

    /* Use a magic formula to calculate enemy spawn thresholds */
    private float calculate_spawn_threshold(int x, float min, float max) {
        return (float) (1.0f - Math.Exp(-((float) x) / spawnAdjustConstant)) * (max - min) + min;   // MATH!!!
    }

    // Update is called once per frame
    void Update() {
        // If this doesn't hold, then something broke somewhere...
        System.Diagnostics.Debug.Assert(activeHazards.Count <= maxHazards);

        if (activeHazards.Count < maxHazards) {
            if (UnityEngine.Random.Range(0, 100) < 50) {
                spawn_enemy();

                totalSpawns++;

                // Adjust croc spawn threshold
                crocThreshold = calculate_spawn_threshold(
                        totalSpawns,
                        CROC_THRESHOLD_START,
                        CROC_THRESHOLD_END
                    );
                    
                // Adjust heron spawn threshold
                heronThreshold = calculate_spawn_threshold(
                        totalSpawns,
                        HERON_THRESHOLD_START,
                        HERON_THRESHOLD_END
                    );
                
                Debug.Log(String.Format("Croc Threshold: {0}", crocThreshold));
                Debug.Log(String.Format("Heron Threshold: {0}", heronThreshold));
                
                // Adjust max hazards
                if (maxHazards < ABSOLUTE_MAX_HAZARDS) {
                    spawnsSinceLastRamp++;

                    if (spawnsSinceLastRamp >= hazardRampThresholds[maxHazards]) {
                        spawnsSinceLastRamp = 0;
                        maxHazards++;
                        Debug.Log(String.Format("maxHazards is now: {0}", maxHazards));
                    }
                }
            }
        }
        else if (activeHazards.Count == maxHazards) {
            clean_up_enemies();
        }

    }
}
