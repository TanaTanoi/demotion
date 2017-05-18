using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LanceHit : MonoBehaviour {

	public Image cross;
	public Text scoreBoard;

	void Start(){
		
	}

	void Update(){
		
	}

	void OnCollisionEnter(Collision other)
	{
		//print ("A collision was detected");
		if (other.gameObject.tag == "Player") {
			Destroy (other.gameObject);
			GameObject ragDoll = (GameObject)Instantiate(Resources.Load("Ragdoll - final"), other.transform.position, other.transform.rotation);
		}
	}
}
