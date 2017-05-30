using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour {
    public static GameController instance;

    /*== PLAYER SETTINGS ==*/
	private Dictionary<int, GameObject> playersDict;  // Dictionary between the player number and the player object.
    
    private PlayerCreator playerCreator;
    private Transform spawnPoints;

    /*== GAME STATUS ==*/
    private bool paused = false;
	private bool playing = false;
    private GameSettings settings;
	private RoundManager roundManager;

	/*== MENU SETTINGS ==*/
	public GameObject menu;  // MenuController handles all the UI elements
	private MenuController menuControl;
    public Text timerText;
    // Access the player hud elements here from a child object of something
    // also get the textbox for the timer here

    private float currentRoundDuration;
	public List<GameObject> crackedTiles;
	public int drop = 0;

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
		playersDict = new Dictionary<int, GameObject> ();
		playerCreator = GetComponent<PlayerCreator> ();
		roundManager = gameObject.AddComponent<DeathMatchRoundManager> ();
        
    }

    public void SetGameSettings(GameSettings gameSettings)
    {
        settings = gameSettings;

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
        for(int i = 0; i < settings.IDtoInput.Count; i++)
        {
            InputType inType;
            settings.IDtoInput.TryGetValue(i, out inType);
            //TODO change the spawn position to be random, change texture to be what the player decided on during customisation
			playersDict.Add(i, playerCreator.CreatePlayer(spawnPoints.GetChild(i).position, inType, i+1));
        }
    }

    /*=== END INITIALISATION ===*/

    /*=== GAME LOGIC ===*/
    /**
     * Starts a new game with the current settings, should only be called after applying game mode
     */
    void StartGame()
    {
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
        SpawnAllPlayers();
		playing = true;
		paused = false;
	}

    // Update is called once per frame
    void FixedUpdate() {
		if (playing && !paused) {
			UpdateTime ();
			if (Input.GetAxisRaw ("Pause") != 0) {
				TogglePause ();
			}
		}
    }

	/**
     * Updates the time remaining of the match
     */
	void UpdateTime()
	{
        // TODO change this to not be called if round duration is infinite
		currentRoundDuration -= Time.deltaTime;
		timerText.text = "Time: " + (int)currentRoundDuration;
		if (currentRoundDuration <= 0)
		{
			PauseGame();
		}

		if ((int)currentRoundDuration == drop) {
			if (crackedTiles.Count > 0) {
				DropCrackedCenter ();
			}
			drop -= 20;
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

	}

    /**
     * Called from a player when they hit another player.
     * Will lose lives or increase score depending on the game mode.
     */
    public void OnHit(int hitter, int hitee)
    {
		 roundManager.OnHit(hitter, hitee);
    }

	/**
     * Reduces the lives remaining of the given player
     */
	private void LoseLife(int playerNumber)
	{
        GameObject player;
        playersDict.TryGetValue(playerNumber, out player);
	}

    /**
     * Increases the score of the given player
     */
    private void IncreaseScore(int playerNumber)
    {

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
}
