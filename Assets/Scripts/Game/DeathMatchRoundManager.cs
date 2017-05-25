using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMatchRoundManager : RoundManager {

	private int scoreIncrement = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/**
	 * Handles what should happen when a player hit another player
	 **/
	public override void onHit(int hitter, int hitee){
		int oldScore = playerScores [hitter];
		playerScores.Add (hitter, oldScore + scoreIncrement);
		updateScoreBoard ();
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
		PlayerCreator pc = new PlayerCreator ();
		pc.CreatePlayer (new Vector3 (0, 0, 0), InputType.Keyboard, 1);
	}
}
