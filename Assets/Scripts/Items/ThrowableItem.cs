using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItem : MonoBehaviour {

	private Transform target;
	private Rigidbody rb; 

	private float fuel;

	private const float MAX_FUEL = 1;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		target = SelectTargetPlayer ();
		fuel = Time.time + MAX_FUEL;
	}

	void FixedUpdate () {
		if (fuel > Time.time) {
			rb.AddForce ((target.position - transform.position).normalized * (rb.velocity.normalized.magnitude * 25));
		}
	}

	public Transform SelectTargetPlayer(){
		PlayerMovement[] players = FindObjectsOfType<PlayerMovement> ();
		Vector3 dir = rb.velocity.normalized;
		float bestDir = -1;
		Transform bestPlayer = players[0].transform;
		foreach (PlayerMovement player in players) {
			Vector3 oDir = (player.transform.position - transform.position).normalized;
			float d = Vector3.Dot (oDir, dir);
			if (bestDir < d) {
				bestDir = d;
				bestPlayer = player.transform;
			}
		}
		return bestPlayer;
	}

}
