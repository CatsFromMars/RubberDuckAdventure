using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HazardGenerator : MonoBehaviour {
    // Yay magic numbers!
    private Vector3 TURTLE_BASE_COORDS = new Vector3(0, -2.6f, 0.25f);
    private Vector3 CROC_BASE_COORDS = new Vector3(0, -4.0f, 0.25f);
    private Quaternion BASE_ROTATION = new Quaternion(-0.7f, 0, 0, 0.7f);

    public Transform SpikedTurtle;
    public Transform Croc;

    private GameObject duck;

    private List<GameObject> activeHazards = new List<GameObject>();
    private const int MAX_HAZARDS = 1;
    
    private const int MIN_SPAWN_DIST = 25;
    private const int MAX_SPAWN_DIST = 50;

    private const int CLEANUP_DIST = 10;

    // Use this for initialization
    void Start() {
        duck = GameObject.Find("Duck");
    }

    /* Randomly choose an enemy to spawn, with proper weighting. */
    Transform choose_enemy_type(out Vector3 enemyBaseCoords) {
        float n = UnityEngine.Random.Range(0, 100);

        if (n < 50.0f) {
            enemyBaseCoords = CROC_BASE_COORDS;
            return Croc;
        }
        else {
            enemyBaseCoords = TURTLE_BASE_COORDS;
            return SpikedTurtle;
        }

    }

    /* Spawn an enemy. Arrrh! */
    void spawn_enemy() {
        float distFromDuck;

        Transform newEnemyTransform;
        GameObject newEnemy = null;

        bool invalid = true;

        int i;

        for (i=0; i<10 && invalid; i++) {
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
                invalid = newEnemy.collider.bounds.Intersects(enemy.collider.bounds);

                if (invalid) {
                    Destroy(newEnemy);  // Blah blah blah this is inefficient blah blah who cares
                    newEnemy = null;
                    break;
                }
            }

        }

        // We must have had an assignment by now!
        System.Diagnostics.Debug.Assert(newEnemy != null);

        activeHazards.Add(newEnemy);
        
        // Otherwise something messed up
        System.Diagnostics.Debug.Assert(activeHazards.Count <= MAX_HAZARDS);

    }

    /* Be a good citizen and clean up when you're finished. */
    void clean_up_enemies() {
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
