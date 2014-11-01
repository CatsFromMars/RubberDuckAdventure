using UnityEngine;
using System.Collections;

public class fire : MonoBehaviour 
{
	CommandCenter test;
	// Use this for initialization
	void Start () 
	{
		test = new CommandCenter();
		test.Main();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
