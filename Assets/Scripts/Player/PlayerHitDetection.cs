using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {

    public float hitThreshold;
    public float invulnerabilityDuration = 5f;

    private float vulnerablility = 0.0f;

    private GameController gameControl;

    private void Start()
    {
        gameControl = FindObjectOfType<GameController>();
    }


    void OnCollisionEnter(Collision collision)
    {
		//Debug.Log("The player hit something");
		Debug.Log (this.gameObject);
        // Do nothing if not hit with a lance or is still invulnerable
        if (!collision.collider.CompareTag("Lance")) return;
        if (Time.time < vulnerablility) return;

        
        // If we have been hit with enough force, play a sound. 
        if(collision.relativeVelocity.magnitude > hitThreshold)
        {
            //Play hit sound here
            Debug.Log("I got hit!");
			ragDoll (collision);
            //gameControl.RemoveLife(GetComponentInParent<PlayerInput>().playerNumber);
            GetComponentInChildren<Rigidbody>().AddRelativeForce(new Vector3(0f, 200f, 0f));
            vulnerablility = Time.time + invulnerabilityDuration;
        }
    }

	void ragDoll(Collision collision){

		for(int i = 0; i < this.gameObject.transform.childCount; i++){
			if (this.gameObject.transform.GetChild (i).CompareTag ("Chair")) {
				//this.gameObject.transform.
			}
			//this.gameObject.GetComponentInChildren<GameObject> ().AddComponent<Rigidbody>();
			//this.gameObject.transform.GetChild (i).parent = null;
		}
		Destroy (this.gameObject);
	}
}

