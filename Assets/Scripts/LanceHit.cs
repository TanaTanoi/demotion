﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LanceHit : MonoBehaviour {


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
			ragDoll.GetComponent<Animator> ().enabled = false;
			// GameController.onHit(this.getPLayer(), getOther(other))
			Debug.Log(getPLayer());
			getOther (other);
		}
	}

	int getPLayer(){
		Debug.Log (this.gameObject.ToString());
		if (this.gameObject.ToString().Equals("Player1 Lance"))
			return 1;
		else if (this.gameObject.ToString().Equals("Player2 Lance"))
			return 2;
		else if (this.gameObject.ToString().Equals("Player3 Lance"))
			return 3;
		else if (this.gameObject.ToString().Equals("Player4 Lance"))
			return 4;
		
		return 0;
	}

	int getOther(Collision other){
		Debug.Log (other.gameObject);
		return 0;
	}


}