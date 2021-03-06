﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {

    public float hitThreshold;
    public float invulnerabilityDuration = 5f;

	private int playerNum;
    private float vulnerablility = 0.0f;

	private float blockAngleThreshold = 0.5f;

    private GameController gameControl;

    private void Start()
    {
        gameControl = FindObjectOfType<GameController>();
    }


    void OnTriggerEnter(Collider other)
    {	
		
		// Do nothing if not hit with a lance or is still invulnerable
		if (!other.GetComponent<Collider>().CompareTag("Player") || Time.time < vulnerablility)
            return;

        // Get the Settings and Movement scripts from this and the other player
		PlayerSettings thisPlayerSettings = gameObject.GetComponentInParent<PlayerMovement>().settings;
		PlayerSettings otherPlayerSettings = other.gameObject.GetComponentInParent<PlayerMovement>().settings;
        PlayerMovement thisPlayerMove = gameObject.GetComponentInParent<PlayerMovement> ();
		PlayerMovement otherPlayerMove = other.gameObject.GetComponentInParent<PlayerMovement> ();

		GetComponent<FMODUnity.StudioEventEmitter>().Play(); // Re add once sound is ready

        if (thisPlayerMove == null)
			return;

		// Get the playerhit and the number of players involved in the collision
		GameObject playerHit = other.gameObject;

		// Debug.Log ("Player hit = " + playerHit);

		//float dotDir = Vector3.Dot (transform.forward, playerHit.transform.forward);
		Vector3 dif = (playerHit.transform.position - transform.position);
		Vector2 dir = new Vector2 (dif.x, dif.z).normalized;
		float dotDif = Vector2.Dot (dir, new Vector2(playerHit.transform.forward.x, playerHit.transform.forward.z).normalized);


	//	Debug.Log(dir + " : " + dotDif + " : " + transform.forward);
		
		// If the direction of the hit is from the front of the player, both get spun.
		//if (Vector3.Dot (transform.forward, playerHit.transform.forward) < blockAngleThreshold) {
		if (dotDif > blockAngleThreshold && Vector3.Dot (transform.forward, -playerHit.transform.forward) < blockAngleThreshold) {
				thisPlayerMove.Boost (-400f);
				otherPlayerMove.Boost (-400f);
				thisPlayerMove.SpinPlayer (Random.Range (-100, 100));
				otherPlayerMove.SpinPlayer (Random.Range (-100, 100));
				return;

		}
			
		GameObject demoteSpawner = (GameObject)Instantiate(Resources.Load("DemoteSpawner"));
		demoteSpawner.transform.position = other.transform.position + Vector3.up * 2.5f;

		// If it reaches here it means thve player should be demoted so OnHit is called

		GameObject ragDoll = gameControl.OnHit(thisPlayerSettings.playerID, otherPlayerSettings.playerID, playerHit);

		Rigidbody[] ragdollrb = ragDoll.GetComponentsInChildren<Rigidbody> ();
		Rigidbody killerb = GetComponentInParent<Rigidbody> ();
		foreach (Rigidbody rb in ragdollrb) {
			rb.AddForce (killerb.velocity * 50);
		}


    }


}
