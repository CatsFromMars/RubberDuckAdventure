using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	public float velocityX = 0.5f;
	public float velocityY = 0f;
	float velocityXLimit = 5f;
	float velocityYLimit = 10f;
	float accelerationX = 0.1f;
	float accelerationY = 0.5f;
	public int damage_level = 0;
	public string tag = "Player";
	public Transform playerBase;
	public Texture duckBase;
	public Texture DuckDamageLevel2;
	public Texture DuckDamageLevel3;
	public Texture[] duckHealth;
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			damage_level += 1;
			if (damage_level == 3) {
				Debug.Log("Player should be dead by now.");
			}
			playerBase.renderer.material.mainTexture = duckHealth[damage_level];
			Debug.Log("FUNCTION CALLED");
		}
	}
	// Use this for initialization
	void Awake () {
<<<<<<< HEAD

=======
		duckHealth = new Texture[3] {duckBase, DuckDamageLevel2, DuckDamageLevel3};
>>>>>>> bb518e95c4a63362e61212ee19417e5c84f549b3
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
