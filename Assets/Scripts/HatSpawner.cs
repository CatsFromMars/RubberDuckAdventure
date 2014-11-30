using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HatSpawner : MonoBehaviour {
    // Yay magic numbers!
    private Vector3 BASE_COORDS = new Vector3(0, -2.6f, 0.25f);
    private Quaternion BASE_ROTATION = new Quaternion(-0.7f, 0, 0, 0.7f);
    
    public List<Transform> hats;

    private GameObject duck;
    
    private GameObject activeHat = null;

    private const int MIN_SPAWN_DIST = 25;
    private const int MAX_SPAWN_DIST = 75;

    private const int CLEANUP_DIST = 10;
    
    // Use this for initialization
    void Start() {
        duck = GameObject.Find("Duck");
    }
    
    /* Randomly choose an enemy to spawn, with proper weighting. */
    private Transform choose_hat_type() {
        int n = (int) UnityEngine.Random.Range(0, hats.Count);
        Debug.Log(String.Format("Spawned hat number {0}", n));
        return hats[n];
    }

    /* Spawn an enemy. Arrrh! */
    private void spawn_hat() {
        //        Debug.Log("Trying to spawn new enemy...");
        
        float distFromDuck;
        
        Transform newHatTransform;

        distFromDuck = UnityEngine.Random.Range(MIN_SPAWN_DIST, MAX_SPAWN_DIST);

        Transform HatType = choose_hat_type();
        
        newHatTransform = (Transform) Instantiate(
            HatType,
            BASE_COORDS + Vector3.right * (get_duck_pos() + distFromDuck),
            BASE_ROTATION
            );
        
        activeHat = newHatTransform.gameObject;

    }
    
    /* Be a good citizen and clean up when you're finished. */
    private void clean_up_hat() {
        if (activeHat.transform.position.x < get_duck_pos() - CLEANUP_DIST) {
            Destroy(activeHat);
            activeHat = null;
        }
    }
    
    /* Get duck X position */
    private float get_duck_pos() {
        return duck.transform.position.x;
    }

    // Update is called once per frame
    void Update() {
        if (activeHat == null) {
            if (UnityEngine.Random.Range(0, 3600) == 1) {
                spawn_hat();
            }
        }
        else {
            clean_up_hat();
        }
        
    }
}
