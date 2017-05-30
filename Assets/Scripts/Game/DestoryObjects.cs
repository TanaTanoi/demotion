using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObjects : MonoBehaviour {

	void OnTriggerEnter(Collider other){



		if (other.gameObject.tag == "Player") {
			Debug.Log ("The object was a player");
			GameObject player = other.gameObject.transform.parent.gameObject;
			GameController gc = GameController.instance;
			RoundManager rm = gc.GetRoundManager ();
			int playerNum = player.GetComponentInChildren<PlayerMovement> ().GetPlayerNum ();
			Destroy (player);
			rm.respawn (playerNum);
		} else {
			Destroy (other.gameObject);
		}
	}
}
