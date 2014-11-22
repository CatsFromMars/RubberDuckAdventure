using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	
    /* --- PRE SETUP --- */
    //duck vars
    public Vector2 curVel;
    public Vector2 toVel;
    private float minY;
    public float acc;
    private float accTemp;
    public float gravity;
    private bool goingUp = false;
    private bool onWater = true;
    public bool isDead = false;
    private float minOffset = 0.5f;
    public PointSystem pointSystem;

    //water control vars
    public Transform waterEffectSpawnPoint; //SPAWNPOINT FOR WATER GRAPHICS
    public Vector2 idleVel; //(0.5,0)
    public Vector2 whirlpoolVel;
    public Vector2 splashVel;
    public Vector2 waveVel;
    //water graphics:
    Transform currentWaterPrefab;
    public Transform whirlpoolPrefab;
    public Transform splashPrefab;
    public Transform wavePrefab;
    Quaternion prefabRot;

    //damage vars
	public int damage_level = 0;
	public int shockedFace = 0;
	public int shockedFaceFrame = 61;
	public string ID = "Player";
    public Quaternion deathRotation;
    public Transform playerBase;
	public Texture duckBase;
	public Texture DuckDamageLevel2;
	public Texture DuckDamageLevel3;
    public Transform playerFace;
	public Texture duckFaceNeutral;
	public Texture duckFaceCrying;
    public Texture duckFaceClosed;
    public Texture[] duckHealth;
    public Texture[] duckFace;
    public ParticleSystem damageParticles;
    public ParticleSystem waterParticles;

    //DUCK ACTION VARIABLES
    private bool isDoingSomething = false;

	/* ----- SETUP ----- */
    void Awake () {
		duckHealth = new Texture[3] {duckBase, DuckDamageLevel2, DuckDamageLevel3};
		duckFace = new Texture[3] {duckFaceNeutral,duckFaceCrying,duckFaceClosed};
        minY = transform.position.y;
        accTemp = acc;
        prefabRot = Quaternion.Euler(-90,0,0);
	}
	
	/* --- MAIN LOOP --- */
    void Update () {

//        Debug.Log(isDoingSomething);

        //PLAYER CAN'T MAKE ANOTHER MOVE UNTIL KEY IS UP
        if(!Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.D) 
           && !Input.GetKeyDown(KeyCode.A)) {
            isDoingSomething = false;
            acc = accTemp;
        }
		
        /* --- water controls --- */
        if (onWater == true && isDead == false && !isDoingSomething) {
            //activate whirlpool when A is pressed
            if (Input.GetKeyDown(KeyCode.A)) {
                isDoingSomething = true;
                accTemp = acc;
                acc = 0;
                curVel = whirlpoolVel;
            }

            //activate splash when W is pressed
            else if (Input.GetKeyDown(KeyCode.W) && isDead == false) { 
                isDoingSomething = true;
                if(currentWaterPrefab != null) Destroy(currentWaterPrefab.gameObject);
                currentWaterPrefab = Instantiate(splashPrefab,waterEffectSpawnPoint.position,prefabRot) as Transform;
                currentWaterPrefab.parent = transform;
                curVel = splashVel;
                goingUp = true;
                onWater = false;
            }
            //activate wave when D is pressed
            else if (Input.GetKeyDown(KeyCode.D) && isDead == false) {
                isDoingSomething = true;
                //SPAWN WATER EFFECT
                if(currentWaterPrefab != null) Destroy(currentWaterPrefab.gameObject);
                currentWaterPrefab = Instantiate(wavePrefab,waterEffectSpawnPoint.position,prefabRot) as Transform;
                currentWaterPrefab.parent = transform;
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
            } else {
                goingUp = false;
            }
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
	
	//Rotate duck if dead
	if (isDead == true){
		deathRotation = Quaternion.Euler(0, 0, 45);
		transform.rotation = Quaternion.Slerp(transform.rotation,deathRotation,2*Time.deltaTime);
	}

	//set duck face's texture, 30 frames per second
	if (shockedFaceFrame == 60 && isDead == false) {
		shockedFace = 0;
		playerFace.renderer.material.mainTexture = duckFace[shockedFace];
	}
	shockedFaceFrame += 1;
	}
	
    void Die() {
    	isDead = true;
    	curVel.x = 0;
    	idleVel.x = 0;
    	curVel.y = -0.01F;
    	idleVel.y = -0.01F;
        int deadFace = 2;
        playerFace.renderer.material.mainTexture = duckFace[deadFace];
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {

            damage_level += 1;

	        shockedFaceFrame = 0;
	        shockedFace = 1;
	        playerFace.renderer.material.mainTexture = duckFace[shockedFace];
            damageParticles.Play();
            if (damage_level >= 3) {
                Debug.Log("Player should be dead by now.");

		        Die();

            }
            if (damage_level < 3)
            {
                playerBase.renderer.material.mainTexture = duckHealth[damage_level];
            }
        }
    }

}


