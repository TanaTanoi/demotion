﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour {
    public static GameController instance;
	private static CameraController mainCamera;

    /*== PLAYER SETTINGS ==*/
   
	private PlayerSettings[] playerSettings;
	private GameObject[] players;

	private PlayerSkins skins;
    public PlayerCreator playerCreator;
    private Transform spawnPoints;

    /*== GAME STATUS ==*/
    private bool paused = false;
	private bool playing = false;
    private GameSettings settings;
	private RoundManager roundManager;
	private GameSetup setup;

	/*== MENU SETTINGS ==*/
	public GameObject menu;  // MenuController handles all the UI elements
	private MenuController menuControl;
    public Text timerText;
    // Access the player hud elements here from a child object of something
    // also get the textbox for the timer here

    private float currentRoundDuration;
	public List<GameObject> crackedTiles;
    public int dropInterval = 20;
	private int drop = 0;

	private float countDown = 3.0f;
	public Text countDownText;

	/*== CAMERA SETTINGS ==*/
	private bool zooming = false;

    /*=== INITIALISATION ===*/


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }

		menuControl = menu.GetComponent<MenuController> ();
		playerCreator = GetComponent<PlayerCreator> ();
		roundManager = gameObject.AddComponent<DeathMatchRoundManager> ();
		mainCamera = FindObjectOfType<CameraController> ();
    }

	public void SetGameSettings(GameSetup setup, GameSettings gameSettings, PlayerSkins newSkins)
    {
		this.setup = setup;
        settings = gameSettings;
		skins = newSkins;
		Debug.Log ("setup");
		playerSettings = new PlayerSettings[settings.playerCount];
		players = new GameObject[settings.playerCount];
		playerCreator.Initialise ();
        Restart();
    }

    public void Restart()
    {
        timerText.text = "Time: " + settings.roundDuration;
        StartGame();
    }

    /**
     * Spawns all the players
     */
    void SpawnAllPlayers()
    {
		spawnPoints = GameObject.Find("SpawnPoints").transform;
        for(int i = 0; i < settings.playerCount; i++)
        {
            //TODO change the spawn position to be random, change texture to be what the player decided on during customisation
			Respawn(i);
        }
    }

    /*=== END INITIALISATION ===*/

    /*=== GAME LOGIC ===*/
    /**
     * Starts a new game with the current settings, should only be called after applying game mode
     */
    void StartGame()
    {
		StartCountDown ();

    }

	void StartCountDown(){
		// count down code
		//menuControl.CountDown();
		SpawnAllPlayers();
		FocusCamera ();
		StartCoroutine (CountDown ());

	}

	IEnumerator CountDown(){
		//menuControl.CountDown();
		yield return new WaitForSeconds(1);
		countDownText.text = "2";
		yield return new WaitForSeconds(1);
		countDownText.text = "1";
		yield return new WaitForSeconds(1);
		countDownText.text = "GO!";
		yield return new WaitForSeconds(1);
		menuControl.DisableCountDown();
		StartRound (1);
	}
    /**
	 * The start of a new round
	 */
    void StartRound(int roundNumber) {
        //TODO display start round splash: 3..2..1..Joust (or something)
        Debug.Log(string.Format("Starting round {0}", roundNumber));
        currentRoundDuration = settings.roundDuration;  // Reset the round duration
		drop = (int)currentRoundDuration - 20;
		Debug.Log ("hello");
		playing = true;
		paused = false;
	}

    // Update is called once per frame
    void FixedUpdate() {
		if (playing && !paused) {
			UpdateTime ();
			FocusCamera ();
		}
    }

	void ShowCountDown(float timeleft){
		
	}

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    /**
     * Updates the time remaining of the match
     */
    void UpdateTime()
	{
        // TODO change this to not be called if round duration is infinite
		currentRoundDuration -= Time.fixedDeltaTime;
		timerText.text = "Time: " + (int)currentRoundDuration;
		if (currentRoundDuration <= 0)
		{
            // TODO end round here
			roundManager.endRound();
		}

		if ((int)currentRoundDuration == drop) {
			if (crackedTiles.Count > 0) {
				DropCrackedCenter ();
			}
			drop -= dropInterval;
		}
	}

	private void FocusCamera(){
		if (!zooming) {
			Vector3 mid = Vector3.zero;
            /* New dict

            int count = IdPlayerDict.CountInner(DictionarySet.first);
            for (int i = 0; i < count; i++)
            {
                mid += IdPlayerDict.GetValue(i).transform.position;
            }
            float biggestDist = 0;
            mid = mid / count;
            for(int i = 0; i < count; i++)
            {
                GameObject p = IdPlayerDict.GetValue(i);
                float distance = Vector3.Distance(p.transform.position, mid);
                biggestDist = Mathf.Max(distance, biggestDist);
            } */
            /* Old dict */
            foreach (GameObject x in players) {
				if(x != null)
					mid += x.transform.position;
			}
			float biggestDist = 0;
			mid = mid / players.Length;

			foreach (GameObject x in players) {
                if (x == null) continue;
				float d = Vector3.Distance (x.transform.position, mid);
				biggestDist = Mathf.Max (d, biggestDist);					
			} 


			mainCamera.SetFocalPoint (mid);
			// 0.3 is the max influence we can get from the base zoom

			mainCamera.desiredBaseZoom = Mathf.Max(0.3f - (biggestDist / 50), 0); // TODO change 50f to some fixed map radius
		}
	}

	/**
     * creates a list of cracked tiles in the arena
     */
	public void CrackedCenterSetup(){
		crackedTiles = GameObject.FindGameObjectsWithTag ("crackedCenter").ToList ();
	}

	/**
     * Drops one cracked tile
     */
	void DropCrackedCenter()
	{
		int randomIndex = Random.Range (0, crackedTiles.Count);
		GameObject crack = crackedTiles [randomIndex];
		crackedTiles.RemoveAt (randomIndex);

		BoxCollider bc = crack.GetComponent<BoxCollider> ();
		bc.isTrigger = true;

		Rigidbody rb = crack.GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		rb.useGravity = true;

		// Plays break sound idk lol
		FMODUnity.StudioEventEmitter sound = rb.transform.gameObject.AddComponent<FMODUnity.StudioEventEmitter> ();
		sound.Event = "event:/FX/environment/floor_destruction";
		sound.Play ();

	}


    /**
     * Called from a player when they hit another player.
     * Will lose lives or increase score depending on the game mode.
     * Returns the ragdoll of the losing player
     */
	public GameObject OnHit(int hitter, int hitee, GameObject playerHit)
    {
		GameObject loser = players [hitee];
		GameObject winner = players [hitter];
		
		Vector3 mid = (winner.transform.position - loser.transform.position) * 0.5f + loser.transform.position;

		StartCoroutine (FocusOnPoint (mid));
		return roundManager.OnHit(hitter, hitee, playerHit);
       
    }

    /**
     * Kills the given player
     */
    public void Kill(GameObject player)
    {	
		roundManager.Suicide (player);
    }


	private IEnumerator FocusOnPoint(Vector3 point)
	{
		zooming = true;
		mainCamera.ZoomIn (point);
		for(int i = 0; i < 6; i++) // Should remove these magic numbers
		{
			Time.timeScale *= 0.8f;
		}
		yield return new WaitForSeconds(1f);
		mainCamera.ReturnZoom ();

		float v = Time.timeScale;
		for(int i = 0; i < 10; i++)
		{
			Time.timeScale = Mathf.Lerp(v, 1, i / 10f);
		}
		zooming = false;

	}


	private void EndRound() {
		playing = false;
	}
    /*=== END GAME LOGIC ===*/

    /*=== PAUSE LOGIC ===*/
    /**
     * Pause/unpause the game
     */
    public void TogglePause()
    {
        if (!paused)
            PauseGame();
        else
            ResumeGame();
    }

    /**
     * Pauses the game, swaps to the pause menu
     */
    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
        menuControl.Pause();
    }

    /**
     * Unpause the game, swaps back to the in game HUD
     */
    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        menuControl.Resume();
    }
    /*=== END PAUSE LOGIC ===*/

	public void DisplayStatBoard(){
		menuControl.StatBoard ();
	}

    // Remove this, only game controller should access the round manager directly
	public RoundManager GetRoundManager(){
		return this.roundManager;
	}

	/**
	 * Manages Respawning of players
	 **/
	public void Respawn (int playerNum){
        if(players[playerNum] != null)
        {
            Destroy(players[playerNum]);
            players[playerNum] = null;
        }
		List<Transform> goodSpawns = new List<Transform> ();
		for (int j = 0; j < spawnPoints.transform.childCount; j++) {
			
			if (IsGoodSpawn (j)) {
				goodSpawns.Add (spawnPoints.transform.GetChild (j));
			}
		}
		int i = Random.Range (0, goodSpawns.Count);
        // Wait for respawn time here
		players[playerNum] = MakePlayer(goodSpawns[i].position, playerNum);

	}

	public GameObject MakePlayer(Vector3 position, int playerNum){
		return playerCreator.CreatePlayer (position, settings.players[playerNum]);
	}

	private bool IsGoodSpawn(int spawnNumber){

		Transform spawn = spawnPoints.GetChild (spawnNumber);
		foreach(GameObject x in players){
			if (x != null) {
				float distance = Vector3.Distance (x.transform.position, spawn.transform.position);
				if (distance < 10) { // Remove magic numbers please
					return false;
				}
			}

		}
		return true;
	}

	public PlayerSkins GetSkins(){
		return skins;
	}
	public PlayerSettings[] GetPlayerSettings(){
		return playerSettings;
	}

	public GameSettings GetSettings() {
		return settings;
	}

	public void ReturnToMenu() {
		Destroy (setup.gameObject);
		menuControl.SwitchScene ("MainMenu");
	}
}
