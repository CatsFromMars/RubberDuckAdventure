﻿using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour {
	private int width, height;
	private int boxWidth, boxHeight;

	public bool visible = true;

	// Use this for initialization
	void Start () {
		width = Screen.width;
		height = Screen.height;
		boxWidth = 300;
		boxHeight = 200;
	} 
	
	// Update is called once per frame
	void OnGUI () {
		if (visible) {
			GUILayout.BeginArea (new Rect((width - boxWidth) / 2, (height + boxHeight) / 2, boxWidth, boxHeight));
			GUILayout.BeginVertical ();
			if (GUILayout.Button ("Continue")) {
				Application.LoadLevel("Test");
			}
			if (GUILayout.Button ("Main Menu")) {
				Application.LoadLevel ("Start_Menu");
			}
			if (GUILayout.Button ("Quit")) {
				Application.Quit();
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}
	}
}
