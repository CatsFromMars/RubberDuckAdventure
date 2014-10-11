using UnityEngine;
using System.Collections;

/// <summary>
/// Moves the waves in a wavy way.
/// </summary>
public class Wavy : MonoBehaviour {
	public GameObject wave1;
	public GameObject wave2;
	public GameObject wave3;

	public Vector3 w1;
	public Vector3 w2; 
	public Vector3 w3;

	// f for frequency
	// p for phase
	// A for amplitude
	private float w1fx = 1.4f;
	private float w1fy = 1.4f;
	private float w1px = 0.2f;
	private float w1py = 0.2f;
	private float w1Ax = 0.2f;
	private float w1Ay = 0.2f;

	private float w2fx = 1.2f;
	private float w2fy = 1.2f;
	private float w2px = 0.2f;
	private float w2py = 0.2f;
	private float w2Ax = 0.2f;
	private float w2Ay = 0.2f;

	private float w3fx = 1.1f;
	private float w3fy = 1.1f;
	private float w3px = 0.1f;
	private float w3py = 0.1f;
	private float w3Ax = 0.1f;
	private float w3Ay = 0.1f;

	private float time = 0.0f;

	// Use this for initialization
	void Start () {
		w1 = wave1.transform.position;
		w2 = wave2.transform.position;
		w3 = wave3.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;

		// wave 1
		wave1.transform.position = w1 + new Vector3(w1Ax*Mathf.Cos(w1fx*time+w1px), 
		                                            w1Ay*Mathf.Cos(w1fy*time+w1py), 0.0f);  

		// wave 2
		wave2.transform.position = w2 + new Vector3(w2Ax*Mathf.Cos(w2fx*time+w2px), 
		                                            w2Ay*Mathf.Cos(w2fy*time+w2py), 0.0f);   

		// wave 3
		wave3.transform.position = w3 + new Vector3(w3Ax*Mathf.Cos(w3fx*time+w3px), 
		                                            w3Ay*Mathf.Cos(w3fy*time+w3py), 0.0f);  
	}
}
