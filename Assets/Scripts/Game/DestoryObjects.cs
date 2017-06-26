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
			control.Kill (other.gameObject);
		} else {
			Destroy (other.gameObject);
		}
	}
}
