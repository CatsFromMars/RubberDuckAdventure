using UnityEngine;
using System.Collections;

public class body_move : MonoBehaviour {

	public GameObject head;
	public float riseBy;
	public float speed;
	Vector3 startPos;
	move headMove;
    public int crocSpeed = 5;

	// Use this for initialization
	void Start () 
	{
		headMove = transform.Find ("CrocLowerJaw").GetComponent("move") as move;
		startPos = transform.position;
		StartCoroutine(moveTo(riseBy, speed));
	}

	IEnumerator moveTo(float byAmount, float inTime)
	{
		Vector3 target = startPos + new Vector3(0.0f, byAmount, 0.0f);

		while (transform.position != target)
		{
			transform.position = Vector3.MoveTowards(transform.position, target, inTime * Time.deltaTime);
			yield return 0;
		}

		headMove.startRotate = true;
	}

	// Update is called once per frame
	void Update () 
	{
        transform.Translate(-1*crocSpeed*Time.deltaTime, 0, 0);
	}
}
