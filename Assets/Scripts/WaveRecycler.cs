using UnityEngine;
using System.Collections;

public class WaveRecycler : MonoBehaviour {
	GameObject[] waves;
	int nextWavesToRecycle = 0;
	
	float lastDuckXPos;
	
	const float RECYCLE_DISP = 25.0f;

	// Use this for initialization
	void Start () {
		waves = new GameObject[] {
			GameObject.Find("WaveCollection0"),
			GameObject.Find("WaveCollection1"),
			GameObject.Find("WaveCollection2"),
		};

		lastDuckXPos = get_duck_pos();

	}
	
	// Update is called once per frame
	void Update () {
		if (get_duck_pos() >= lastDuckXPos + RECYCLE_DISP) {
			recycle_waves();
		}
	}

	// Get duck X position
	private float get_duck_pos() {
		GameObject duck = GameObject.Find("Duck");
		return duck.transform.position.x;
	}
	
	void recycle_waves() {
		Debug.Log("Recycling waves...");
		waves[nextWavesToRecycle].transform.Translate(75, 0, 0);
		
		nextWavesToRecycle++;
		nextWavesToRecycle %= 3;
		
		lastDuckXPos = get_duck_pos();
		
	}

}
