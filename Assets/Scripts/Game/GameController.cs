using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	// Singleton GameController
	public static GameController instance = null;

    /*== PLAYER SETTINGS ==*/
	// Dictionary between the player number and the player object.
	private Dictionary<int, GameObject> playersDict;
	// Empty game object holding the possible spawn points for other players
	public Transform spawnPoints;

	// Game status
    private bool paused = true;
	private bool playing = false;

	/*== MENU settings ==*/

	// MenuController handles all the UI elements
    private MenuController menuControl;
	// List of HUD elements where the players' lives are shown. // Change this to not drag and drop?
	public GameObject[] playersHUD = new GameObject[4];


    // REMOVE ME!!
    public GameObject prefabToSpawn; // This will be replaced with the return from the PlayerCreator


	// Round Settings
	private float roundDuration;
	private static GameMode mode;
	public Text timerText;

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
        InitialiseControls();

	}

    /**
     * Gets the number of input controllers connected
     */
    void InitialiseControls()
    {
        string[] controllers = Input.GetJoystickNames();

    }

	/**
	 * The start of a new round
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
     * Reduces the lives remaining of the given player
     */
	public void LoseLife(int playerNumber)
	{

	}

    /**
     * Increases the score of the given player
     */
    public void IncreaseScore(int playerNumber)
    {

    }
    /*=== End Game Logic ===*/
}
