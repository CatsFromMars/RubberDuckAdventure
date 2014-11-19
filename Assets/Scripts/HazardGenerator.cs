using UnityEngine;
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
    private const int MAX_HAZARDS = 5;
    
    private const int MIN_SPAWN_DIST = 25;
    private const int MAX_SPAWN_DIST = 75;

    // Minimum distance between spawned enemies.
    private const int MIN_ENEMY_DIST = 8;

    private const int CLEANUP_DIST = 10;

    // Use this for initialization
    void Start() {
        duck = GameObject.Find("Duck");
    }

    /* Randomly choose an enemy to spawn, with proper weighting. */
    private Transform choose_enemy_type(out Vector3 enemyBaseCoords) {
        float n = UnityEngine.Random.Range(0, 100);

        if (n < 25.0f) {
            enemyBaseCoords = CROC_BASE_COORDS;
            return Croc;
        }
        else if (n < 50.0f) {
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
        Debug.Log("Trying to spawn new enemy...");

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
            Debug.Log("Spawned new enemy!");
        }
        
        // Otherwise something messed up
        System.Diagnostics.Debug.Assert(activeHazards.Count <= MAX_HAZARDS);

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

    // Update is called once per frame
    void Update() {
        // If this doesn't hold, then something broke somewhere...
        System.Diagnostics.Debug.Assert(activeHazards.Count <= MAX_HAZARDS);

        if (activeHazards.Count < MAX_HAZARDS) {
            if (UnityEngine.Random.Range(0, 100) < 50) {
                spawn_enemy();
            }
        }
        else if (activeHazards.Count == MAX_HAZARDS) {
            clean_up_enemies();
        }

    }
}
