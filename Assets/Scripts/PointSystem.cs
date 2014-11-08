using UnityEngine;
using System.Collections;

public class PointSystem : MonoBehaviour {
        float score;
        public Transform duck;
	public PlayerBehavior duckBehavior;
        
	// Use this for initialization
	void Awake () {
            score = 0;
            duckBehavior = duck.GetComponent<PlayerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
            if (duckBehavior.isDead == false) {
                score = Mathf.Floor(duck.position.x + 11);
            }
            guiText.text = "Score: " + score.ToString();
        }
}
