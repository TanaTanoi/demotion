using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMatchRoundManager : RoundManager {

	private int scoreIncrement = 1;
	private int numOfPlayers = 2;
	private Transform spawnPoints;
	// Use this for initialization
	void Start () {
		playerScores = new Dictionary<int, int> ();
		for (int i = 1; i <= numOfPlayers; i++) {
			playerScores.Add (i, 0);
		}
		spawnPoints = GameObject.Find("SpawnPoints").transform;
		hud = GameObject.Find ("Menu").GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/**
	 * Handles what should happen when a player hit another player
	 **/
	public override void onHit(int hitter, int hitee){
		int oldScore = playerScores [hitter];
		playerScores.Remove (hitter);
		playerScores.Add (hitter, oldScore + scoreIncrement);
		updateScoreBoard ();
		respawn (hitee);
	}

	/**
	 * Checks to see if the current round is still playing
	 **/
	public override bool isRoundOver(){
		return false; // there for compiling atm
	}

	/**
	 * Checks to see if any of the players have meet the win condition or if the round/time limit has been reached
	 **/
	public override bool isGameOver(){
		return false; // there for compiling atm
	}

	/**
	 * Called when the round is over, facilitates starting the next round
	 **/
	protected override void endRound(){

	}

	/**
	 * Manages Respawning of players
	 **/
	public override void respawn (int playerNum){
		Debug.Log ("The respawn method in Deathmatch was called");
		int i = Random.Range (0, spawnPoints.transform.childCount -1);
		GameObject newPlayer = (GameObject)Instantiate(Resources.Load("PlayerPrefab - final"), spawnPoints.GetChild(i).position, spawnPoints.GetChild(i).rotation);
		PlayerMovement pm = newPlayer.GetComponentInChildren<PlayerMovement> ();
		pm.SetPlayerNum (playerNum);
	}
}
