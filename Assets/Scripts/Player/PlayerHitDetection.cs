using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {

    public float hitThreshold;
    public float invulnerabilityDuration = 5f;

	private int playerNum;
    private float vulnerablility = 0.0f;

    private GameController gameControl;

    private void Start()
    {
        gameControl = FindObjectOfType<GameController>();
    }


    void OnTriggerEnter(Collider other)
    {
		GameObject playerHit = other.gameObject;
		Debug.Log("The player hit something");
		Debug.Log (this.gameObject);
        // Do nothing if not hit with a lance or is still invulnerable
        if (!other.GetComponent<Collider>().CompareTag("Player")) return;
        if (Time.time < vulnerablility) return;

        
        // If we have been hit with enough force, play a sound. 
		//if(playerHit.relativeVelocity.magnitude > hitThreshold)
        //{
            //Play hit sound here
            Debug.Log("I got hit!");
		GameObject chair = (GameObject)playerHit.transform.Find ("chairA").gameObject;
		playerHit.transform.Find ("chairA").parent = null;
		chair.AddComponent<Rigidbody> ();

		GameObject lance = playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject;
//		GameObject lance = (GameObject)playerHit.transform.Find ("Lance").gameObject;
		playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject.transform.parent = null;
		lance.AddComponent<Rigidbody> ();

		Destroy(playerHit);
		GameObject ragDoll = (GameObject)Instantiate(Resources.Load("Ragdoll - final"), other.transform.position, other.transform.rotation);
		int otherPlayerNum = other.gameObject.GetComponent <PlayerMovement> ().GetPlayerNum ();

            //gameControl.RemoveLife(GetComponentInParent<PlayerInput>().playerNumber);
        playerHit.GetComponentInChildren<Rigidbody>().AddRelativeForce(new Vector3(0f, 200f, 0f));
        vulnerablility = Time.time + invulnerabilityDuration;
       // }
    }

	public int GetPlayerNum(){
		return this.playerNum;
	}
}
