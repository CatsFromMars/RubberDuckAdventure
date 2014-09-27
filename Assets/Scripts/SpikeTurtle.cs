using UnityEngine;
using System.Collections;

public class SpikeTurtle: MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            //other.gameObject.health -= 1 should be added when Player is more clearly defined.
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
