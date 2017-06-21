using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObjects : MonoBehaviour {

    private GameController control;
    

    private void Start()
    {
        control = GameController.instance;
    }

    void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			
			//GameObject player = other.gameObject.transform.parent.gameObject;
            control.Kill(other.gameObject);
			
			//int playerNum = player.GetComponentInChildren<PlayerMovement> ().GetPlayerNum ();
			//Destroy (player);
			//rm.respawn (playerNum);
		}
	}
}
