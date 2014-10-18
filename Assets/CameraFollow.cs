using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform camera;
    public Transform follow;
    public Vector2 speed;

    private Vector3 cp;

    void Start() { cp = follow.position; }

	//Slowly move camera to target
	void Update () {
        cp.x = Mathf.Lerp(cp.x,follow.position.x,Time.deltaTime*speed.x);
        cp.y = Mathf.Lerp(cp.y,follow.position.y,Time.deltaTime*speed.y);
        camera.position = cp;
	}
}
