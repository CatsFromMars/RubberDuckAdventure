using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SceneryGenerator : MonoBehaviour {
    // Yay magic numbers!
    private Vector3 BASE_COORDS = new Vector3(0, 10.0f, 8.0f);
    private Quaternion BASE_ROTATION = new Quaternion(-0.7f, 0, 0, 0.7f);

    private const int MAX_Z_VARIANCE = 5;
    private const int MAX_Y_VARIANCE = 5;

    public Transform Cloud;

    private GameObject duck;

    private List<GameObject> activeScenery = new List<GameObject>();
    private const int MAX_SCENERY = 3;
    
    private const int MIN_SPAWN_DIST = 30;
    private const int MAX_SPAWN_DIST = 200;

    private const int CLEANUP_DIST = 20;

    // Use this for initialization
    void Start() {
        duck = GameObject.Find("Duck");
    }

    /* Make some new random scenery coordinates */
    private Vector3 new_random_coords(float xPos) {
        return BASE_COORDS + new Vector3(
                xPos,
                UnityEngine.Random.Range(0, MAX_Y_VARIANCE),
                UnityEngine.Random.Range(0, MAX_Z_VARIANCE)
            );
    }

    /* Spawn some scenery. Ahhhh... */
    void spawn_scenery() {
        Debug.Log("Spawning new scenery...");

        float distFromDuck;

        Transform newSceneryTransform;
        GameObject newScenery = null;

        distFromDuck = UnityEngine.Random.Range(MIN_SPAWN_DIST, MAX_SPAWN_DIST);

        newSceneryTransform = (Transform) Instantiate(
                Cloud,
                new_random_coords(get_duck_pos() + distFromDuck),
                BASE_ROTATION
            );

        newScenery = newSceneryTransform.gameObject;

        activeScenery.Add(newScenery);
        
        // Otherwise something messed up
        System.Diagnostics.Debug.Assert(activeScenery.Count <= MAX_SCENERY);

    }

    /* Be a good citizen and clean up when you're finished. */
    void clean_up_scenery() {
        float duckPos = get_duck_pos();

        for (int i=0; i<activeScenery.Count; i++) {
            GameObject scenery = activeScenery[i];

            if (scenery.transform.position.x < duckPos - CLEANUP_DIST) {
                Debug.Log("Cleaning up off-screen scenery...");
                activeScenery.RemoveAt(i);
                Destroy(scenery);
            }
        }
        
    }
    
    /* Get duck X position */
    private float get_duck_pos() {
        return duck.transform.position.x;
    }

    // Update is called once per frame
    void Update() {
//        Debug.Log(String.Format("Count is {0}", activeScenery.Count));
        
        // If this doesn't hold, then something broke somewhere...
        System.Diagnostics.Debug.Assert(activeScenery.Count <= MAX_SCENERY);

        if (activeScenery.Count < MAX_SCENERY) {
            spawn_scenery();
        }
        else if (activeScenery.Count == MAX_SCENERY) {
            clean_up_scenery();
        }

    }
}
