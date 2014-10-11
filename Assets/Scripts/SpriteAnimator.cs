using UnityEngine;
using System.Collections;

//A CRUDE ANIMATION SCRIPT

public class SpriteAnimator : MonoBehaviour {
	private int index;
	public Texture2D[] frames;
	public float fps = 10f;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float frameVal = Time.time * fps;
		index = (int) frameVal;
		index = index % frames.Length;
		renderer.material.mainTexture = frames[index];

	}
}
