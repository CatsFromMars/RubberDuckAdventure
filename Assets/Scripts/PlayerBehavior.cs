using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	public float velocityX = 0.5f;
	public float velocityY = 0f;
	float velocityXLimit = 5f;
	float velocityYLimit = 10f;
	float accelerationX = 0.1f;
	float accelerationY = 0.5f;
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
		//WHIRLPOOL INPUT
		if (Input.GetKey(KeyCode.A))
		{
			Whirlpool();
		}

		//WAVE INPUT
		if (Input.GetKeyDown(KeyCode.D))
		{
			Wave();
		}
		
	}

	void Whirlpool() {
		velocityX = 0;
	}

	void Wave() {
		velocityY = velocityYLimit;
		velocityX = velocityXLimit;
	}

	void Move() {
		//MOVE TO THE RIGHT
		transform.Translate(Time.deltaTime*velocityX, Time.deltaTime*velocityY, 0);

		//ACCELTERATE
		if (velocityX < velocityXLimit) velocityX += accelerationX;

		//DECELERATE
		if (velocityY > 0) velocityY -= accelerationY;
	}

}
