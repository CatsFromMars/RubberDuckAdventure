using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {
    private GameObject duck;
    private GameObject[] waves;
	
	private int nextWavesToRecycle = 0;
	
	private float lastDuckXPos;
	
	private float WAVE_WIDTH;

	// Use this for initialization
    void Start() {
        duck = GameObject.Find("Duck");

        lastDuckXPos = get_duck_pos();

		waves = new GameObject[] {
			GameObject.Find("WaveCollection0"),
			GameObject.Find("WaveCollection1"),
			GameObject.Find("WaveCollection2"),
		};

		MeshFilter waveMesh = (MeshFilter) GameObject.Find("PlaneWave1").GetComponent("MeshFilter");
		WAVE_WIDTH = waveMesh.collider.bounds.size.x;

	}
	
	// Update is called once per frame
	void Update() {
		if (get_duck_pos() >= lastDuckXPos + WAVE_WIDTH) {
			recycle_waves();
		}
	}

	// Get duck X position
	private float get_duck_pos() {
		return duck.transform.position.x;
	}
	
	void recycle_waves() {
//		Debug.Log("Recycling waves...");
		waves[nextWavesToRecycle].transform.Translate(WAVE_WIDTH * 3, 0, 0);
		
		nextWavesToRecycle++;
		nextWavesToRecycle %= 3;
		
		lastDuckXPos += WAVE_WIDTH;
		
	}

}
