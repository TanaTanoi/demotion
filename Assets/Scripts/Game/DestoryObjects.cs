using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObjects : MonoBehaviour {

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Player") {
			// do something with the player
		} else {
			Destroy (other.gameObject);
		}
	}
}
