using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("The start method was called");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log("The lance hit something");
		if (collision.gameObject.tag == "Player") {

			Destroy (collision.gameObject);
		}
	}
}
