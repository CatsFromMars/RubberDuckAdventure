using UnityEngine;
using System.Collections;

public class Croc : MonoBehaviour {

	public GameObject lowerJaw;
	public float riseBy;
	public float speed;

    CrocJaw jaw;

    private enum CrocState {
        LURKING,
        RISING,
        CHARGING
    };

    private CrocState state = CrocState.LURKING;

	private Vector3 startPos;   // Croc starting position
    private Vector3 targetPos;  // Croc position after rising

    public int crocSpeed = 5;

    // Distance at which Croc will charge the duck
    private float CHARGE_DIST = 18.0f;

    private GameObject duck;

	// Use this for initialization
	void Start () 
	{
		jaw = (CrocJaw) lowerJaw.GetComponent("CrocJaw");
		
        startPos = transform.position;
        targetPos = startPos + new Vector3(0.0f, riseBy, 0.0f);

        duck = GameObject.Find("Duck");

	}
    
    /* Get duck X position */
    private float get_duck_pos() {
        return duck.transform.position.x;
    }

    /* Move the croc up per frame, while RISING */
	private void rise() {
		transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
		
        if (transform.position.y >= targetPos.y) {
            jaw.begin_rotate();
            state = CrocState.CHARGING;
        }
	}

	// Update is called once per frame
	void Update () 
	{
        switch (state) {
            case CrocState.LURKING:
                audio.Play();
                if (get_duck_pos() > transform.position.x - CHARGE_DIST) {
                    state = CrocState.RISING;
                }
                break;

            case CrocState.RISING:
                rise();
                break;

            case CrocState.CHARGING:
                transform.Translate(-1 * crocSpeed * Time.deltaTime, 0, 0);
                break;
        }

	}
}
