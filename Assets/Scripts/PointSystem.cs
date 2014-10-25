using UnityEngine;
using System.Collections;

public class PointSystem : MonoBehaviour {
        float score;
        public Transform duck;
        
	// Use this for initialization
	void Awake () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
            score = Mathf.Floor(duck.position.x + 11);
            guiText.text = "Score: " + score.ToString();
        }
}
