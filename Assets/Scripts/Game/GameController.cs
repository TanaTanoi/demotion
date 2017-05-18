using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    public enum GameMode { DEATHMATCH, HIGHSCORE };

	// Singleton GameController
	public static GameController instance = null;

    /*== PLAYER SETTINGS ==*/
	// Dictionary between the player number and the player object.
	private Dictionary<int, GameObject> playersDict;
	// Empty game object holding the possible spawn points for other players
	public Transform spawnPoints;
    private PlayerCreator playerCreator;

	/*== GAME STATUS ==*/
    private bool paused = true;
	private bool playing = false;

	/*== MENU SETTINGS ==*/
	// MenuController handles all the UI elements
    private MenuController menuControl;
    public Text timerText;
	// Access the player hud elements here from a child object of something
    // also get the textbox for the timer here

	/*== ROUND SETTINGS ==*/
	private GameMode mode;
    private float roundDuration;
    private int maxLives;
    private int maxScore;
    

    /*=== Initialisation ===*/
    private void Awake()
    {
		// GameController is a singleton therefore we need to ensure there is only one
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
        
        // Don't kill me :(
        DontDestroyOnLoad(gameObject);
    }

	void Start() {
        playerCreator = GetComponent<PlayerCreator>();
        InitialiseControls();
        
	}

    /**
     * Gets the number of input controllers connected
     */
    void InitialiseControls()
    {
        string[] controllers = Input.GetJoystickNames();
        playerCreator.CreatePlayer(spawnPoints.GetChild(0), InputType.Keyboard);
    }

	/**
	 * The start of a new round
	 */
	void StartGame() {


	}

    // Update is called once per frame
    void Update() {
		if (playing) {
			UpdateTime ();
			if (Input.GetAxisRaw ("Pause") != 0) {
				TogglePause ();
			}
		}

        
    }
    /*=== End Initialisation ===*/
    

    /*=== Game Logic ===*/

    public void TogglePause()
    {
        paused = !paused;
        if (paused)
            PauseGame();
        else
            ResumeGame();
    }
     
    /**
     * Pauses the game and opens the pause menu
     */
    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
        //FindObjectOfType<MenuController>().TogglePause();
        // show the hud
        menuControl.Pause();
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        // Hide the hud
        menuControl.Resume();
    }

    /**
     * Creates a new game from the game settings
     */
    public void NewGame()
    {
//        // Pause the game
//        PauseGame();
//        // Enable player HUDS for each player in the game
//		for (int i = 0; i < ; i++)
//        {
//            playersHUD[i].SetActive(true);
//        }

        // Spawn all the players
    }

	/**
     * Updates the time remaining of the match
     */
	void UpdateTime()
	{
		roundDuration -= Time.deltaTime;
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
            case GameMode.DEATHMATCH:
                LoseLife(hitee);
                break;
            case GameMode.HIGHSCORE:
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
    /*=== End Game Logic ===*/
}
