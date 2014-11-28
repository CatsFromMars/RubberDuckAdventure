using UnityEngine;
using System.Collections;

public class SpikedTurtle: MonoBehaviour {


    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") audio.Play();
    }

}
