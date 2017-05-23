using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
    public static GameController instance;

    /*== PLAYER SETTINGS ==*/
	private Dictionary<int, GameObject> playersDict;  // Dictionary between the player number and the player object.
    public GameObject playerPrefab;
    private PlayerCreator playerCreator;
    private Transform spawnPoints;

    /*== GAME STATUS ==*/
    private bool paused = false;
    private GameSetup setup;
    private GameSettings settings;

	/*== MENU SETTINGS ==*/
    public MenuController menuControl;  // MenuController handles all the UI elements
    private Text timerText;
    // Access the player hud elements here from a child object of something
    // also get the textbox for the timer here

    private float currentRoundDuration;


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

        spawnPoints = GameObject.Find("SpawnPoints").transform;
        Debug.Log(spawnPoints != null);
        playerCreator = gameObject.AddComponent<PlayerCreator>() as PlayerCreator;
        playerCreator.SetPlayerPrefab(playerPrefab);
        setup = GetComponent<GameSetup>();
    }

    public void SetGameSettings(GameSettings settings)
    {
        this.settings = settings;

		StartGame ();
    }

    /**
     * Spawns all the players
     */
    void SpawnAllPlayers()
    {

        for(int i = 0; i < settings.IDtoInput.Count; i++)
        {
            InputType inType;
            settings.IDtoInput.TryGetValue(i, out inType);
            //TODO change the spawn position to be random, change texture to be what the player decided on during customisation
            playersDict.Add(i, playerCreator.CreatePlayer(spawnPoints.GetChild(i).position, inType, 1));
        }
    }

    /*=== END INITIALISATION ===*/

    /*=== GAME LOGIC ===*/
    /**
     * Starts a new game with the current settings, should only be called after applying game mode
     */
    void StartGame()
    {
        // Play each round
        for(int i = 0; i < settings.numberRounds; i++)
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
        currentRoundDuration = settings.roundDuration;  // Reset the round duration
        SpawnAllPlayers();
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
		//timerText.text = "Time: " + (int)settings.roundDuration;
		if (currentRoundDuration <= 0)
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
        switch(settings.mode)
        {
            case GameSetup.GameMode.DEMOTION:
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
