using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMatchRoundManager : RoundManager {
	
	private int scoreIncrement = 1;
	private int numOfPlayers = 4;
	private Transform spawnPoints;
	

	void Start () {
		playerScores = new Dictionary<int, int> ();
		playerKills = new Dictionary<int, int> ();
		playerDeaths = new Dictionary<int, int> ();
		playerSuicides = new Dictionary<int, int> ();
		playerSprees = new Dictionary<int, int> ();
		bestSprees = new Dictionary<int, int> ();
//		for (int i = 0; i <= numOfPlayers; i++) {
//			playerScores.Add (i, 0);
//		}
		initPlayers(4);
		spawnPoints = GameObject.Find("SpawnPoints").transform;
		hud = GameObject.Find ("Menu").GetComponent<Canvas> ();
		targetScore = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRoundOver ()) {
			endRound ();
		}
	}

	/**
	 * Handles what should happen when a player hit another player
	 * Returns the ragdoll that is hit
	 **/

	public override GameObject OnHit(int hitter, int hitee, GameObject playerHit){
		
		GameObject ragdoll = RagDoll (playerHit);
		UpdateStats (hitter, hitee);
		updateScoreBoard ();
		Respawn (hitee);
		return ragdoll;
	}
		

	/**
	 * Checks to see if the current round is still playing
	 **/
	public override bool isRoundOver(){
		foreach (int playerNum in playerScores.Keys){
			if (playerScores[playerNum] >= targetScore)
				return true;
		}
		return false;
	}

	/**
	 * Checks to see if any of the players have meet the win condition or if the round/time limit has been reached
	 **/
	public override bool isGameOver(){
		return false; // there is no need for this method in a game mode with only one round
	}

	/**
	 * Called when the round is over, facilitates starting the next round
	 **/
	protected override void endRound(){
		Debug.Log ("The round has ended");
		updateStatBoard ();
		StartCoroutine (ShowScoreboard (2f));
	}

	private IEnumerator ShowScoreboard(float delay){
		yield return new WaitForSeconds (delay);
		GameController.instance.DisplayStatBoard ();
		Time.timeScale = 0;
	}

	/**
	 * Manages Respawning of players
	 **/
	public override void Respawn (int playerNum){
		GameController.instance.Respawn (playerNum);
	}

	/**
	 * Handle when a player falls off the map
	 */ 
	public override void Suicide (GameObject player){

		// get the player number and reduce their score
		PlayerSettings playerSettings  = player.GetComponentInParent<PlayerMovement>().settings;
		int playerNum = playerSettings.playerID;
		int oldScore = playerScores [playerNum];
		int oldSuicides = playerSuicides [playerNum];
		int oldDeaths = playerDeaths [playerNum];
		playerScores.Remove (playerNum);
		playerSuicides.Remove (playerNum);
		playerDeaths.Remove (playerNum);
		playerScores.Add (playerNum, oldScore - scoreIncrement);
		playerSuicides.Add (playerNum, oldSuicides + 1);
		playerDeaths.Add (playerNum, oldDeaths + 1);
		updateScoreBoard ();

		// make the player ragdoll
		RagDoll(player);

		// respawn
		Respawn(player.GetComponentInParent<PlayerMovement>().settings.playerID);
	}
}
