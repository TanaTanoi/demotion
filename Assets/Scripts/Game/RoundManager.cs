using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public abstract class RoundManager : MonoBehaviour {

	private int scoreIncrement = 1;
	private int numOfPlayers = 4;

	protected Dictionary<int,int> playerScores; // dictionary of player numbers to player score

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
	public abstract GameObject OnHit (int hitter, int hitee, GameObject playerHit);

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
	public abstract void endRound ();

    /**
     * Handle respawning of players
     */
	public abstract IEnumerator Respawn(int playerNum);

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
		
	protected void UpdateStats(int hitter, int hitee){
		int oldScore = playerScores [hitter];
		int oldKills = playerKills [hitter];
		int oldDeaths = playerDeaths [hitee];
		int spree = playerSprees [hitter];
		playerScores.Remove (hitter);
		playerKills.Remove (hitter);
		playerDeaths.Remove (hitee);
		playerSprees.Remove (hitter);
		playerSprees.Remove (hitee);
		playerScores.Add (hitter, oldScore + scoreIncrement);
		playerKills.Add (hitter, oldKills + 1);
		playerDeaths.Add (hitee, oldDeaths + 1);
		playerSprees.Add (hitter, spree + 1);
		playerSprees.Add (hitee, 0);
		if (playerSprees [hitter] > bestSprees [hitter]) {
			bestSprees.Remove (hitter);
			bestSprees.Add (hitter, playerSprees [hitter]);
		}
		Debug.Log ("Player " + hitter + " is on a " + playerSprees [hitter] + " kill spree");
	}

	protected void UpdateStatsSuicide(int playerNum){
		int oldScore = playerScores [playerNum];
		int oldSuicides = playerSuicides [playerNum];
		int oldDeaths = playerDeaths [playerNum];
		playerScores.Remove (playerNum);
		playerSuicides.Remove (playerNum);
		playerDeaths.Remove (playerNum);
		playerSprees.Remove (playerNum);
		playerScores.Add (playerNum, oldScore - scoreIncrement);
		playerSuicides.Add (playerNum, oldSuicides + 1);
		playerDeaths.Add (playerNum, oldDeaths + 1);
		playerSprees.Add (playerNum, 0);
		updateScoreBoard ();
	}

	/**
	 * Call this method whenever the stats board needs updating
	 **/
	protected void updateStatBoard(){
		Debug.Log ("Updating Stat Board");
		Text[] text = hud.GetComponentsInChildren<Text> ();
		for(int i = 0; i < text.Length; i++){
			for (int j = 1; j <= 4; j++) {

				if (text [i].name.Equals ("Player " + j + " Score")) {
					text [i].text = playerScores [j-1].ToString ();
					Debug.Log ("Player score updated");
				}

				if (text [i].name.Equals ("Player " + j + " Promotions")) {
					text [i].text = playerKills [j-1].ToString ();
				}

				if (text [i].name.Equals ("Player " + j + " Work Place Accidents")) {
					text [i].text = playerSuicides [j-1].ToString ();
				}

				if (text [i].name.Equals ("Player " + j + " Work Place Accidents")) {
					text [i].text = playerSuicides [j-1].ToString ();
				}

				if (text [i].name.Equals ("Player " + j + " Demotions")) {
					text [i].text = playerDeaths [j-1].ToString ();
				}

				if (text [i].name.Equals ("Player " + j + " Spree")) {
					text [i].text = bestSprees [j-1].ToString ();
				}
			}
		}

	}

	/**
	 * makes a ragdoll and unparents the lance and chair
	 **/
	protected GameObject RagDoll(GameObject playerHit){

		// The top level of the player prefab, this ensures we are dealing with the parent object everytime
		Transform parent = playerHit.transform.root;

		// get the wrapper as this has the chair and material as chidlren
		Transform wrapper = parent.transform.Find ("wrapper");

		// deparent the lance
		GameObject lance = playerHit.GetComponentInChildren<PlayerHitDetection> ().gameObject;
		Destroy (lance.GetComponent<PlayerHitDetection> ());
		lance.transform.parent = null;
		lance.AddComponent<Rigidbody> ();

		// unparent the chair
		Transform chair = wrapper.Find ("chairA");
		chair.parent = null;
		chair.gameObject.AddComponent<Rigidbody> ();

		// get the material
		Material ragdollMat = wrapper.Find("PlayerModel").GetComponent<Renderer> ().material;

		// create the ragdoll, position it and apply the material
		//Quaternion q = Quaternion.Euler(-parent.transform.forward);
		GameObject ragDoll = (GameObject)Instantiate(Resources.Load("Ragdoll - final"), parent.transform.position, parent.transform.rotation);
		//ragDoll.transform.rotation = Quaternion.Euler (parent.transform.rotation.eulerAngles);
		ragDoll.GetComponentInChildren<Renderer> ().materials = new Material[] { ragdollMat, ragdollMat};

		PlayerLimbs limbs = ragDoll.GetComponent<PlayerLimbs> ();

		// Setting the player customisation using player limbs
		GameObject hat = parent.GetComponentInChildren<Hat> ().gameObject;
		//GameObject playerHat = Instantiate (hat, limbs.head.transform.position, limbs.head.transform.rotation);
		hat.transform.position = limbs.head.transform.position;
		hat.transform.rotation = limbs.head.transform.rotation;
		hat.transform.parent = limbs.head.transform;
	

		// destroy the old payer model
		Destroy(parent.gameObject);
		return ragDoll;
	}
}
