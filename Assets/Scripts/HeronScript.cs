using UnityEngine;
using System.Collections;

public class HeronScript : MonoBehaviour {

    /* --- PRE SETUP --- */
    public Texture[] HeronTex;
    //transition vars
    private float animTimer;
    public float animTime;
    //down vars
    private float downTimer;
    public float downTime;
    //up vars
    private float upTimer;
    public float upTime;
    //anim vars
    public bool goingDown = false;
    private bool peak = false;
    public int frame; // Ranges from 0-9

    /* ----- SETUP ----- */
    void Start () {
        if(frame == 0) { goingDown = true; peak = true; }
        if(frame == 9) { goingDown = false; peak = true; }
    }
   
    /* --- MAIN LOOP --- */
	void Update () {
	  
        //alternating between 0 - 9
        if(animTimer <= 0 && peak == false) {
            //if the heron is going down:
            if(goingDown) {
                //in transition:
                if(0 < frame && frame < 9) { animTimer = animTime; }
                //to down pos:
                if (frame == 8) {
                    downTimer = downTime;
                    goingDown = false;   
                    peak = true;
                }
                //to next frame:
                frame += 1;
            }
            //the frame is going up:
            else {
                //in transition:
                if(0 < frame && frame < 9) { animTimer = animTime; } 
                //to up pos:
                if (frame == 1) {
                    upTimer = upTime;
                    goingDown = true;
                    peak = true;
                }
                //down pos
                frame -= 1;
            }
        }
        //at down:
        if(downTimer <= 0 && frame == 9) {
            animTimer = animTime;
            peak = false;
            frame -= 1;
        }
        //at up:
        if(upTimer <= 0 && frame == 0) {
            animTimer = animTime;
            peak = false;
            frame +=1;
        }

        //set the frame of the heron:
        renderer.material.SetTexture("_MainTex", HeronTex[frame]);

        //decrement the frame timers:
        if(animTimer > 0) { animTimer -= Time.deltaTime; }
        if(downTimer > 0) { downTimer -= Time.deltaTime; }
        if(upTimer > 0) { upTimer -= Time.deltaTime; }

        /* --- PLAYER DAMAGE CODE HERE --- */
        if(frame == 9) { collider.enabled = true; } 
        else { collider.enabled = false; }

	}

}




