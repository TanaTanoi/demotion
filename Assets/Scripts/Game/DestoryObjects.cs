using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObjects : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		Debug.Log ("collision");

		if (other.gameObject.tag == "Player") {
			// do something with the player
		} else {
			Debug.Log ("hello");
			Destroy (other.gameObject);
		}
	}
}
