using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class RoundManager : MonoBehaviour {
	
	protected Dictionary<int,int> playerScores; // dictionary of player numbers to player score -- change to be handled by the concrete version
    // -- Many of these are in the GameSettings object and dont need to be stored twice --
	protected int numOfRounds;
	protected int currentRound;
	protected int targetScore;
	protected int maxRoundDuration;
	protected Canvas hud;

	void Start () {
		playerScores = new Dictionary<int, int> ();
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
		}
	}

	/**
	 * Handles what should happen when a player hit another player
	 **/
	public abstract void OnHit (int hitter, int hitee);

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
}
