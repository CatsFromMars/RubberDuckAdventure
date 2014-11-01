using UnityEngine;
using System;
using System.Collections;

public class move : MonoBehaviour 
{
	public GameObject jaw;
	private Vector3 maxAngle;
	public int rotateSpeed = 40;
	public int seconds = 2;
	public bool startRotate = false;

	// Use this for initialization
	void Start () {
		maxAngle = new Vector3(0.0f,0.0f,-1.0f) * 200;
	}

	IEnumerator Rotate(Vector3 byAngle, float inTime)
	{
		if (Math.Abs(jaw.transform.eulerAngles[2]) >= Math.Abs(maxAngle[2]))
		{
			yield break;
		}
		Quaternion fromAngle = jaw.transform.rotation;
		Quaternion toAngle = Quaternion.Euler(jaw.transform.eulerAngles + byAngle);
		for (float t = 0.0f; t < 1; t += Time.deltaTime/inTime)
		{
			jaw.transform.rotation = Quaternion.Lerp (fromAngle, toAngle, t);
			yield return null;
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if (startRotate)
		{
			StartCoroutine (Rotate (new Vector3(0.0f,0.0f,-1.0f) * rotateSpeed, seconds));
		}
	}
}
