using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    GameObject player;
    Texture hatTexture;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hatTexture = this.gameObject.GetComponent<MeshRenderer>().materials[0].GetTexture(0);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Transform hat = player.transform.FindChild("DuckHatLayer");
            Debug.Log(hat.tag);
            MeshRenderer mesh = hat.gameObject.GetComponent<MeshRenderer>();

            hat.renderer.sharedMaterial.SetTexture(0,hatTexture);

            //texture = this.gameObject.GetComponent<MeshRenderer>().material.GetTexture(0);
            Destroy(this.gameObject);
        }
    }
}
