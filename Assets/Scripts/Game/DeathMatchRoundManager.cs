using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMatchRoundManager : RoundManager {
	
	private int scoreIncrement = 1;
	private int numOfPlayers = 4;
	private Transform spawnPoints;
	

	void Start () {
		playerScores = new Dictionary<int, int> ();
		for (int i = 0; i <= numOfPlayers; i++) {
			playerScores.Add (i, 0);
		}
		spawnPoints = GameObject.Find("SpawnPoints").transform;
		hud = GameObject.Find ("Menu").GetComponent<Canvas> ();
		targetScore = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRoundOver ()) {
			endRound ();
		}
	}

	/**
	 * Handles what should happen when a player hit another player
	 **/
	public override void OnHit(int hitter, int hitee){
		Debug.Log ("Player " + hitter + " hit Player " + hitee);
		int oldScore = playerScores [hitter];
		playerScores.Remove (hitter);
		playerScores.Add (hitter, oldScore + scoreIncrement);
		updateScoreBoard ();
		Respawn (hitee);
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
	}

	/**
	 * Manages Respawning of players
	 **/
	public override void Respawn (int playerNum){
		GameController.instance.Respawn (playerNum);
	}
}
