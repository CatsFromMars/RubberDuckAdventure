using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	public float velocityX = 1f;
	public float velocityY = 0f;
	float velocityXLimit = 3f;
	float velocityYLimit = 5f;
	float accelerationX = 0.1f;
	float accelerationY = 3f;
	int damage_level;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	
	}

	void FixedUpdate() {
		GetInput();

	}

	void GetInput() {
		if (Input.GetButton("Whirlpool"))
		{
			velocityX = 0;
		}

	}

	void Move() {
		//MOVE TO THE RIGHT
		transform.Translate(Time.deltaTime*velocityX, Time.deltaTime*velocityY, 0);

		//ACCELTERATE
		if(velocityX < velocityXLimit) velocityX += accelerationX;
	}
}
