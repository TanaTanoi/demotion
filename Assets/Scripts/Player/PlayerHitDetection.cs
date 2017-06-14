using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {

    public float hitThreshold;
    public float invulnerabilityDuration = 5f;

	private int playerNum;
    private float vulnerablility = 0.0f;

	private float blockAngleThreshold = -0.3f;

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

        // Get the Settings and Movement scripts from this and the other player
		PlayerSettings thisPlayerSettings = gameObject.GetComponentInParent<PlayerMovement>().settings;
		PlayerSettings otherPlayerSettings = other.gameObject.GetComponentInParent<PlayerMovement>().settings;
        PlayerMovement thisPlayerMove = gameObject.GetComponentInParent<PlayerMovement> ();
		PlayerMovement otherPlayerMove = other.gameObject.GetComponentInParent<PlayerMovement> ();

        if (thisPlayerMove == null)
			return;

		// Get the playerhit and the number of players involved in the collision
		GameObject playerHit = other.gameObject;

		// If the direction of the hit is from the front of the player, both get spun.
		if (Vector3.Dot (-transform.forward, -playerHit.transform.forward) > blockAngleThreshold) {
			thisPlayerMove.Boost (-400f);
			otherPlayerMove.Boost (-400f);
			thisPlayerMove.SpinPlayer (Random.Range(-100, 100));
			otherPlayerMove.SpinPlayer (Random.Range(-100, 100));
			return;
		}

        // Get the lance and chair and unparent them so they remain in the game scene

        Transform chair = playerHit.transform.Find ("chairA");
		chair.parent = null;
		chair.gameObject.AddComponent<Rigidbody> ();
		GameObject lance = playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject;
		Destroy (lance.GetComponent<PlayerHitDetection> ());
		playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject.transform.parent = null;
		lance.AddComponent<Rigidbody> ();

        // Call the Onhit mehtod and destroy the other player and replace with ragdoll
        if (gameControl.OnHit(thisPlayerSettings.playerID, otherPlayerSettings.playerID))
        {
            GameObject destroyThis = playerHit.transform.parent.gameObject;
            Destroy(destroyThis);
            GameObject ragDoll = (GameObject)Instantiate(Resources.Load("Ragdoll - final"), other.transform.position, other.transform.rotation);

            vulnerablility = Time.time + invulnerabilityDuration; // Should be controlled by settings probably
        }
      
    }


}
