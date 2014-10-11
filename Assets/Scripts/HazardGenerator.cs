using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HazardGenerator : MonoBehaviour {
    // Yay magic numbers!
    private Vector3 BASE_COORDS = new Vector3(0, -2.6f, 0.25f);
    private Quaternion BASE_ROTATION = new Quaternion(-0.7f, 0, 0, 0.7f);

    public Transform SpikedTurtle;

    private List<GameObject> activeHazards = new List<GameObject>();
    private const int MAX_HAZARDS = 5;

    // Use this for initialization
    void Start() {

    }

    // Spawn an enemy. Arrrh!
    void spawn_enemy() {
        Debug.Log("Spawning new enemy...");

        float distFromDuck;

        Transform newEnemyTransform;
        GameObject newEnemy = null;

        bool invalid = true;

        int i;

        for (i=0; i<10 && invalid; i++) {
            distFromDuck = UnityEngine.Random.Range(30, 70);

            newEnemyTransform = (Transform) Instantiate(
                    SpikedTurtle,
                    BASE_COORDS + Vector3.right * (get_duck_pos() + distFromDuck),
                    BASE_ROTATION
                );

            newEnemy = newEnemyTransform.gameObject;

            invalid = false;

            foreach (GameObject enemy in activeHazards) {
                invalid = newEnemy.collider.bounds.Intersects(enemy.collider.bounds);

                if (invalid) {
                    Debug.Log("Spawned colliding enemy! Trying again...");
                    Destroy(newEnemy);  // Blah blah blah this is inefficient blah blah who cares
                    newEnemy = null;
                    break;
                }

            }

            if (!invalid) {
                Debug.Log("Spawned enemy that does not collide.");
            }

        }

        if (i == 10) {
            Debug.Log("Could not place a non-colliding enemy! Giving up...");
        }

        // We must have had an assignment by now!
        System.Diagnostics.Debug.Assert(newEnemy != null);

        activeHazards.Add(newEnemy);
        
        // Otherwise something messed up
        System.Diagnostics.Debug.Assert(activeHazards.Count <= MAX_HAZARDS);

    }

    // Be a good citizen and clean up when you're finished.
    void clean_up_enemy() {
        Debug.Log("Cleaning up off-screen enemy...");

        GameObject hazardToDestroy = activeHazards[0];

        activeHazards.RemoveAt(0);

        Destroy(hazardToDestroy);
        
    }
    
    // Get duck X position
    private float get_duck_pos() {
        return GameObject.Find("Duck").transform.position.x;
    }

    // Update is called once per frame
    void Update() {
//        Debug.Log(String.Format("Count is {0}", activeHazards.Count));

        if (activeHazards.Count < MAX_HAZARDS) {
            if (UnityEngine.Random.Range(0, 100) < 50) {
                spawn_enemy();
            }
        }
        else if (activeHazards.Count == MAX_HAZARDS) {
            if (activeHazards[0].transform.position.x < get_duck_pos() - 10) {
                clean_up_enemy();
            }
        }

    }
}
