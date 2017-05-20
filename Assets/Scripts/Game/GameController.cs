using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	// Singleton GameController
	public static GameController instance = null;

    /*== PLAYER SETTINGS ==*/
	// Dictionary between the player number and the player object.
	private Dictionary<int, GameObject> playersDict;
    private int playerCount;
    // Empty game object holding the possible spawn points for other players
    public Transform spawnPoints;
    private PlayerCreator playerCreator;

	/*== GAME STATUS ==*/
    private bool paused = false;

	/*== MENU SETTINGS ==*/
	// MenuController handles all the UI elements
    private MenuController menuControl;
    private Text timerText;
    // Access the player hud elements here from a child object of something
    // also get the textbox for the timer here

    /*== ROUND SETTINGS ==*/
    private GameSetup.GameMode mode;
    private int numberRounds;
    private float roundDuration;
    private int maxLives;
    private int maxScore;

    private float currentRoundDuration;


    /*=== INITIALISATION ===*/
    private void Awake()
    {
		// GameController is a singleton therefore we need to ensure there is only one
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
    }

	void Start() {
        //Find the menu and the player creator
        menuControl = FindObjectOfType<MenuController>();
        playerCreator = GetComponent<PlayerCreator>();
	}

    /**
     * Gets the number of input controllers connected
     */
    void SpawnPlayers()
    {
        // Get the number of possible players between 2 and 4.
        
        for(int i = 0; i < playerCount; i++)
        {
            playerCreator.CreatePlayer(spawnPoints.GetChild(i).position, InputType.Controller, 1);
        }
    }

    /**
     * Creates a new game from the game settings
     */
    public void ApplyGameMode(GameSetup.GameMode mode, int numberRounds, float roundDuration, int maxLives, int maxScore)
    {
        this.mode = mode;
        this.numberRounds = numberRounds;
        this.roundDuration = roundDuration;
        this.maxLives = maxLives;
        this.maxScore = maxScore;

        StartGame();
    }
    /*=== END INITIALISATION ===*/

    /*=== GAME LOGIC ===*/
    /**
     * Starts a new game with the current settings, should only be called after applying game mode
     */
    void StartGame()
    {
        // Play each round
        for(int i = 0; i < numberRounds; i++)
        {
            StartRound(i);
            //TODO Display scoreboard here ==
        }
    }

    /**
	 * The start of a new round
	 */
    void StartRound(int roundNumber) {
        //TODO display start round splash: 3..2..1..Joust (or something)
        Debug.Log(string.Format("Starting round {0}", roundNumber));
        currentRoundDuration = roundDuration;  // Reset the round duration
        SpawnPlayers();
	}

    // Update is called once per frame
    void FixedUpdate() {
		if (!paused) {
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
		currentRoundDuration -= Time.deltaTime;
		timerText.text = "Time: " + (int)roundDuration;

		if (roundDuration <= 0)
		{
			PauseGame();
		}
	}

    /**
     * Called from a player when they hit another player.
     * Will lose lives or increase score depending on the game mode.
     */
    public void OnHit(int hitter, int hitee)
    {
        switch(mode)
        {
            case GameSetup.GameMode.DEATHMATCH:
                LoseLife(hitee);
                break;
            case GameSetup.GameMode.HIGHSCORE:
                IncreaseScore(hitter);
                break;
        }
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
