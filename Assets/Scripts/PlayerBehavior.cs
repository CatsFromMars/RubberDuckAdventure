using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	
    /* --- PRE SETUP --- */
    //duck vars
    public Vector2 curVel;
    public Vector2 toVel;
    private float minY;
    public float acc;
    public float gravity;
    private bool goingUp = false;
    private bool onWater = true;

    //water control vars
    public Vector2 idleVel; //(0.5,0)
    public Vector2 whirlpoolVel;
    public Vector2 splashVel;
    public Vector2 waveVel;
    //water graphics:
    public Transform whirlpool;
    public Transform splash;
    public Transform wave;

    //damage vars
	public int damage_level = 0;
	public string ID = "Player";
    public Transform playerBase;
	public Texture duckBase;
	public Texture DuckDamageLevel2;
	public Texture DuckDamageLevel3;
    public Texture[] duckHealth;
    public ParticleSystem damageParticles;

	/* ----- SETUP ----- */
    void Awake () {
		duckHealth = new Texture[3] {duckBase, DuckDamageLevel2, DuckDamageLevel3};
        minY = transform.position.y;
	}
	
	/* --- MAIN LOOP --- */
    void Update () {
		
        /* --- water controls --- */
        if (onWater == true) {
            //activate whirlpool when A is pressed
            if (Input.GetKeyDown(KeyCode.A)) {
                curVel = whirlpoolVel;
            }
            //activate splash when W is pressed
            else if (Input.GetKeyDown(KeyCode.W)) { 
                curVel = splashVel;
                goingUp = true;
                onWater = false;
            }
            //activate wave when D is pressed
            else if (Input.GetKeyDown(KeyCode.D)) {
                curVel = waveVel;
                goingUp = true;
                onWater = false;
            }
        }

        /* --- Now move the duck according to velocity. --- */

        //if going up, slow down to halt.
        if(goingUp == true) {
            if (Mathf.Abs(curVel.x - idleVel.x) > 0.1f) {
                //accelerate the duck's velocity to target velocity
                curVel.y = Mathf.Lerp(curVel.y,idleVel.y,Time.deltaTime*acc);
            } else { goingUp = false; }
        //now going down:
        } else {
            if (transform.position.y > minY) {
                curVel.y -= Time.deltaTime*gravity;
            } else { 
                curVel.y = idleVel.y; 
                onWater = true;
            }
        }

        //slow down current x velocity to idle.
        curVel.x = Mathf.Lerp(curVel.x,idleVel.x,Time.deltaTime*acc);

        //move the duck according to current velocity
        transform.Translate(curVel.x,curVel.y,0);

	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            damage_level += 1;
            damageParticles.Play();
            if (damage_level >= 3) {
                Debug.Log("Player should be dead by now.");
                damage_level %= 3;  // TODO: Get rid of this
            }
            playerBase.renderer.material.mainTexture = duckHealth[damage_level];
            //          Debug.Log("FUNCTION CALLED");
        }
    }

}


