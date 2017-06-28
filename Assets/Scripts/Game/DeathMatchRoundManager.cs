using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathMatchRoundManager : RoundManager {
	
	private int scoreIncrement = 1;
	private int numOfPlayers = 4;
	private Transform spawnPoints;
	private float RespawnDelay;
	private bool playing  = true;

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
		targetScore = GameController.instance.GetSettings().targetScore;
		RespawnDelay = GameController.instance.GetSettings ().respawnTime;
		Debug.Log ("target Score: " + targetScore);
	}
	
	// Update is called once per frame
	void Update () {
		if (isRoundOver () && playing) {
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
		StartCoroutine(Respawn (hitee));
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
	public override void endRound(){
		playing = false;
		StartCoroutine (ShowScoreboard (6f));
	}

	private IEnumerator ShowScoreboard(float delay){

		yield return new WaitForSeconds (2);
		GameController gc = FindObjectOfType<GameController>();
		List<int> playerIds = new List<int> ();
		playerIds.AddRange (playerScores.Keys);
		playerIds = playerIds.OrderBy( x =>  playerScores[x] ).ToList();
		playerIds.Reverse ();
		List<int> topThree = new List<int> ();
		for (int i = 0; i < 3; i++) {
			if (playerScores [playerIds [i]] > 0)
				topThree.Add (playerIds [i]);
		}
		FindObjectOfType<GameFinished> ().FinishGame (topThree, gc.GetPlayerSettings()); 

		updateStatBoard ();
		yield return new WaitForSeconds (delay);
		GameController.instance.DisplayStatBoard ();
		Time.timeScale = 0;
	}

	/**
	 * Manages Respawning of players
	 **/
	public override IEnumerator Respawn (int playerNum){
		yield return new WaitForSeconds (RespawnDelay);
		GameController.instance.Respawn (playerNum);
	}

	/**
	 * Handle when a player falls off the map
	 */ 
	public override void Suicide (GameObject player){

		// get the player number and reduce their score
		PlayerSettings playerSettings  = player.GetComponentInParent<PlayerMovement>().settings;
		int playerNum = playerSettings.playerID;
		UpdateStatsSuicide (playerNum);
		// make the player ragdoll
		RagDoll(player);

		// respawn
		StartCoroutine(Respawn(playerNum));
	}
}
