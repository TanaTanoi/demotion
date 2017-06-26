using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public abstract class RoundManager : MonoBehaviour {
	
	protected Dictionary<int,int> playerScores; // dictionary of player numbers to player score -- change to be handled by the concrete version

	protected Dictionary<int,int> playerKills;
	protected Dictionary<int,int> playerDeaths;
	protected Dictionary<int,int> playerSuicides;
	protected Dictionary<int,int> playerSprees;
	protected Dictionary<int,int> bestSprees;


    // -- Many of these are in the GameSettings object and dont need to be stored twice --
	protected int numOfRounds;
	protected int currentRound;
	protected int targetScore;
	protected int maxRoundDuration;
	protected Canvas hud;

	void Start () {
		playerScores = new Dictionary<int, int> ();
		playerKills = new Dictionary<int, int> ();
		playerDeaths = new Dictionary<int, int> ();
		playerSuicides = new Dictionary<int, int> ();
		playerSprees = new Dictionary<int, int> ();
		bestSprees = new Dictionary<int, int> ();
		hud = GameObject.Find ("Menu").GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isRoundOver ()) {
			endRound ();
		}
	}

	/**
	 * Creates the number of players passed in an initalises them with a score of zero
	 **/
	public void initPlayers(int numOfPlayers){
		for (int i = 0; i <= numOfPlayers; i++) {
			playerScores.Add(i,0);
			playerKills.Add (i, 0);
			playerDeaths.Add (i, 0);
			playerSuicides.Add (i, 0);
			playerSprees.Add(i, 0);
			bestSprees.Add(i, 0);
		}
	}

	/**
	 * Handles what should happen when a player hit another player
	 **/
	public abstract void OnHit (int hitter, int hitee, GameObject playerHit);

	/**
	 * Checks to see if the current round is still playing
	 **/
	public abstract bool isRoundOver ();

	/**
	 * Checks to see if any of the players have meet the win condition or if the round/time limit has been reached
	 **/
	public abstract bool isGameOver ();

	/**
	 * Called when the round is over, facilitates starting the next round
	 **/
	protected abstract void endRound ();

    /**
     * Handle respawning of players
     */
    public abstract void Respawn(int playerNum);

	/**
	 * Handle when a player falls off the map
	 */ 
	public abstract void Suicide (GameObject player);

	/**
	 * Call this method whenever the scoreboard UI needs to be updated 
	 **/
	protected void updateScoreBoard(){
		
		Text[] text = hud.GetComponentsInChildren<Text> ();
		for(int i = 0; i < text.Length; i++){
			if(text[i].name.Equals("Player1Score")){
				text [i].text = playerScores [0].ToString ();
			}
			if(text[i].name.Equals("Player2Score")){
				text [i].text = playerScores[1].ToString();
			}
			if(text[i].name.Equals("Player3Score")){
				text [i].text = playerScores [2].ToString ();
			}
			if(text[i].name.Equals("Player4Score")){
				text [i].text = playerScores[3].ToString();
			}
		}

	}

	protected void RagDoll(GameObject playerHit){

		// Get the top level of the player prefab
		Transform parent = playerHit.transform;
		while (parent.parent != null) {
			parent = parent.parent;
		}

		// get the wrapper as this has the chair and material as chidlren
		Transform wrapper = parent.transform.Find ("wrapper");

		// unparent the chair
		Transform chair = wrapper.Find ("chairA");
		chair.parent = null;
		chair.gameObject.AddComponent<Rigidbody> ();

		// get the material
		Material ragdollMat = wrapper.Find("PlayerModel").GetComponent<Renderer> ().material;

		// deparent the lance
		GameObject lance = playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject;
		Destroy (lance.GetComponent<PlayerHitDetection> ());
		playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject.transform.parent = null;
		lance.AddComponent<Rigidbody> ();

		// destroy the old payer model
		Destroy(playerHit);

		// create the ragdoll, position it and apply the material
		Quaternion q = Quaternion.Euler(-parent.transform.forward);
		GameObject ragDoll = (GameObject)Instantiate(Resources.Load("Ragdoll - final"), parent.transform.position, q);
		ragDoll.transform.rotation = Quaternion.Euler (parent.transform.rotation.eulerAngles);
		ragDoll.GetComponentInChildren<Renderer> ().materials = new Material[] { ragdollMat, ragdollMat};
	}
}
