using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Transform hat = player.transform.FindChild("DuckHatLayer");
            MeshRenderer mesh = hat.gameObject.GetComponent<MeshRenderer>();
            Texture texture = mesh.material.GetTexture(0);
            texture = this.gameObject.GetComponent<MeshRenderer>().material.GetTexture(0);
            Destroy(this.gameObject);
        }
    }
}
