using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LanceHit : MonoBehaviour {

	FixedJoint playerHolding;

	void Start(){
		playerHolding = this.GetComponent<FixedJoint> ();
	}

	void Update(){
		if (playerHolding.connectedBody == null) {
			Destroy (this);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		//print ("A collision was detected");
		if (other.gameObject.tag == "Player") {
			Destroy (other.gameObject);
			GameObject ragDoll = (GameObject)Instantiate(Resources.Load("Ragdoll - final"), other.transform.position, other.transform.rotation);
			ragDoll.GetComponent<Animator> ().enabled = false;
			GameObject chair = (GameObject) other.transform.Find ("Chair").gameObject;
			chair.transform.parent = null;
			chair.AddComponent<Rigidbody>();
			// GameController.onHit(this.getPLayer(), getOther(other))
			Debug.Log(getPLayer());
			Debug.Log(getOther (other));
		}
	}

	/**
	 * Gets the player number for the player holding the lance
	 **/
	int getPLayer(){
		if (this.gameObject.name.Equals("Player1 Lance"))
			return 1;
		else if (this.gameObject.name.Equals("Player2 Lance"))
			return 2;
		else if (this.gameObject.name.Equals("Player3 Lance"))
			return 3;
		else if (this.gameObject.name.Equals("Player4 Lance"))
			return 4;
		
		return 0;
	}

	/**
	 * Gets the player number for the player who got hit by the lance
	 **/
	int getOther(Collision other){
		if (other.gameObject.name.Equals("Player1"))
			return 1;
		else if (other.gameObject.name.Equals("Player2"))
			return 2;
		else if (other.gameObject.name.Equals("Player3"))
			return 3;
		else if (other.gameObject.name.Equals("Player4"))
			return 4;
		return 0;
	}


}
