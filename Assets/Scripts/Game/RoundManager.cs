﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoundManager : MonoBehaviour {

	protected Dictionary<int,int> playerScores; // dictionary of player numbers to player score
	private int numOfRounds;
	private int currentRound;
	private int targetScore;
	private int maxRoundDuration;
	private Canvas playerScoreCanvas;

	void Start () {
		playerScores = new Dictionary<int, int> ();
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
		for (int i = 1; i <= numOfPlayers; i++) {
			playerScores.Add(1,0);
		}
	}

	/**
	 * Handles what should happen when a player hit another player
	 **/
	public abstract void onHit (int hitter, int hitee);

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
	 * Manages Respawning of players
	 **/
	public abstract void respawn (int playerNum);

	/**
	 * Call this method whenever the scoreboard UI needs to be updated 
	 **/
	protected void updateScoreBoard(){
		
	}
}