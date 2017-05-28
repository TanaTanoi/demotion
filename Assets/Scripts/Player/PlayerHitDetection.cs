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
		if (!other.GetComponent<Collider>().CompareTag("Player")) return;
		if (Time.time < vulnerablility) return;
		GameObject playerHit = other.gameObject;
		Debug.Log (other.gameObject);
        // Do nothing if not hit with a lance or is still invulnerable
		int thisPlayer = this.transform.gameObject.GetComponentInParent<PlayerMovement> ().GetPlayerNum ();
		int otherPlayer = other.gameObject.GetComponentInParent<PlayerMovement>().GetPlayerNum();

         
		GameObject chair = (GameObject)playerHit.transform.Find ("chairA").gameObject;
		playerHit.transform.Find ("chairA").parent = null;
		chair.AddComponent<Rigidbody> ();

		GameObject lance = playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject;
//		GameObject lance = (GameObject)playerHit.transform.Find ("Lance").gameObject;
		playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject.transform.parent = null;
		lance.AddComponent<Rigidbody> ();
		gameControl.OnHit(thisPlayer, otherPlayer);
		Destroy(playerHit);
		GameObject ragDoll = (GameObject)Instantiate(Resources.Load("Ragdoll - final"), other.transform.position, other.transform.rotation);
//		int otherPlayerNum = other.gameObject.GetComponent <PlayerMovement> ().GetPlayerNum ();

        playerHit.GetComponentInChildren<Rigidbody>().AddRelativeForce(new Vector3(0f, 200f, 0f));
        vulnerablility = Time.time + invulnerabilityDuration;
       // }
    }

	public int GetPlayerNum(){
		return this.playerNum;
	}
}
