﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {

    public float hitThreshold;
    public float invulnerabilityDuration = 5f;

	private int playerNum;
    private float vulnerablility = 0.0f;

	private float blockAngleThreshold = -0.8f;

    private GameController gameControl;

    private void Start()
    {
        gameControl = FindObjectOfType<GameController>();
    }


    void OnTriggerEnter(Collider other)
    {	
		// Do nothing if not hit with a lance or is still invulnerable
		if (!other.GetComponent<Collider>().CompareTag("Player")) return;
		if (Time.time < vulnerablility) return;

		PlayerMovement thisPlayerMove = this.transform.gameObject.GetComponentInParent<PlayerMovement> ();
		PlayerMovement otherPlayerMove = other.gameObject.GetComponentInParent<PlayerMovement> ();
		if (thisPlayerMove == null)
			return;

		// Get the playerhit and the number of players involved in the collision
		GameObject playerHit = other.gameObject;

		// If the direction of the hit is from the front of the player, both get spun.
		if (Vector3.Dot (-transform.forward, -playerHit.transform.forward) > blockAngleThreshold) {
			thisPlayerMove.Boost (-100f);
			otherPlayerMove.Boost (-100f);
			thisPlayerMove.SpinPlayer (Random.Range(-100, 100));
			otherPlayerMove.SpinPlayer (Random.Range(-100, 100));
			return;
		}

		int thisPlayerNum = thisPlayerMove.GetPlayerNum ();
		int otherPlayerNum = otherPlayerMove.GetPlayerNum();

        // Get the lance and chair and unparent them so they remain in the game scene
		GameObject chair = (GameObject)playerHit.transform.Find ("chairA").gameObject;
		playerHit.transform.Find ("chairA").parent = null;
		chair.AddComponent<Rigidbody> ();
		GameObject lance = playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject;
		Destroy (lance.GetComponent<PlayerHitDetection> ());
		playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject.transform.parent = null;
		lance.AddComponent<Rigidbody> ();

		// Call the Onhit mehtod and destroy the other player and replace with ragdoll
		gameControl.OnHit(thisPlayerNum, otherPlayerNum);
		GameObject destroyThis = playerHit.transform.parent.gameObject;
		Destroy(destroyThis);
		GameObject ragDoll = (GameObject)Instantiate(Resources.Load("Ragdoll - final"), other.transform.position, other.transform.rotation);

        vulnerablility = Time.time + invulnerabilityDuration;
      
    }


}
