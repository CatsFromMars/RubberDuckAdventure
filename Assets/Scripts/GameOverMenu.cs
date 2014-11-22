using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour {
	private int width, height;
	private int boxWidth, boxHeight;
	public float deadPlace;

	public bool visible = false;
	
	public Transform duck;
	PlayerBehavior behavior;

	void Awake() {
		
		behavior = duck.GetComponent<PlayerBehavior>();
	}
	
	// Use this for initialization
	void Start () {
		width = Screen.width;
		height = Screen.height;
		boxWidth = 300;
		boxHeight = 200;
		deadPlace = 0.0F;
	} 
	
	// Update is called once per frame
	void OnGUI () {
		if (behavior.isDead == true){
			if (deadPlace <= 0.1F){
				deadPlace = Time.time;
			}
			else if (Time.time - deadPlace >= 2.0F)
			{
				visible = true;
			}
		}
		if (visible) {
			guiText.text = "Game Over";
			GUILayout.BeginArea (new Rect((width - boxWidth) / 2, (height + boxHeight) / 2 - 100, boxWidth, boxHeight));
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
