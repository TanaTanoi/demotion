using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public enum GameMode { DEATHMATCH, SCORE }

	// Singleton GameController
	public static GameController instance = null;

    // PLAYER SETTINGS
	// Dictionary between the player number and the player object.
	private Dictionary<int, GameObject> playersDict;
	// Empty game object holding the possible spawn points for other players
	public Transform spawnPoints;

	// Game status
    private bool paused = true;
	private bool playing = false;

	// MENU settings
	// MenuController handles all the UI elements
    private MenuController menuControl;
	// List of HUD elements where the players' lives are shown. // Change this to not drag and drop?
	public GameObject[] playersHUD = new GameObject[4];


	// Round Settings
	private float roundDuration;
	private static GameMode mode;
	public Text timerText;


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
		// Call the player factory to create player 1
		//GameObject player1 = PlayerCreator.InstanciatePlayer(Vector3.zero, InputType.Keyboard, 0);
		playersDict.Add(1, player1);

	}

	/**
	 * The start of a new Round
	 */
	void StartGame(GameMode gameMode) {
		mode = gameMode;

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
     * Restarts the game, resetting the time, players, lives, etc
     */
    public void RestartGame()
    {
//        // Pause the game
//        PauseGame();
//        // Enable player HUDS for each player in the game
//		for (int i = 0; i < ; i++)
//        {
//            playersHUD[i].SetActive(true);
//        }
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
     * Reduces the lives remaining of the given player
     */
	public void LoseLife(int playerNumber)
	{

	}

	/**
	 * Sets the Game mode
	 */
	public void SetGameMode(GameMode gameMode) {
		mode = gameMode;
	}

	/**
	 * Sets the duration of the round
	 */
	public void SetRoundDuration(int duration) {
		roundDuration = duration;
	}

    
}
