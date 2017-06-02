using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour {
    public static GameSetup instance;

    //Game type enums
    public enum GameMode { DEMOTION, HIGHSCORE, LASTWORKERSITTING, FIRSTTOKILLS, FIRSTTOSCORE };
    /**
     * ========================================
     * GAME MODES:
     * ----------
     * Default settings:
     *   . Round duration
     *   . Number of rounds
     *   . Respawn time
     * 
     * DEMOTION:
     *   Team with the most demotions at the end of the match wins.
     *     RELEVANT SETTINGS:
     *       . Default settings
     *       
     *       
     * HIGHSCORE:
     *   Team with the highest score at the end of the match wins.
     *     RELEVANT SETTINGS:
     *       . Default settings
     *       
     *       
     * LAST WORKER SITTING:
     *   Last team in the game is the winner.
     *     RELEVANT SETTINGS:
     *       . Default settings
     *       + Maximum lives
     *       - Respawn time
     *       
     *       
     * FIRST TO KILLS:
     *   Team who reaches a certain number of kills first is the winner (of the round?).
     *     RELEVANT SETTINGS:
     *       . Default settings
     *       + Target kills
     *   
     *   
     * FIRST TO SCORE:
     *   Team that reaches a certain score first is the winners (of the round?).
     *     RELEVANT SETTINGS:
     *       . Default settings
     *       + Target score
     * 
     * 
     * ========================================
     */

	/*== Settings Menu Text ==*/
    public GameObject settingsPanel;
	private Text roundsText;
	private Text durationText;
	private Text respawnText;
	private Text livesText;
	private Text targetKillsText;
	private Text targetScoreText;


	private GameObject controller;  // GameController gameobject
    private GameController control; // GameController script
    private GameSettings settings;  // Local settings applied via menu
	private ArenaGenerator generator;  // Arena generator, could this be moved to the game controller?
    private bool settingUp = false;  // Setting up boolean so players can set teams

    // Use this for initialization
    void Awake() {
        DontDestroyOnLoad(gameObject);

		Text[] slidersText = settingsPanel.GetComponentsInChildren<Text> (true);
		roundsText = slidersText [2];
		durationText = slidersText [3];
		respawnText = slidersText [4];
		livesText = slidersText [5];
		targetKillsText = slidersText [6];
		targetScoreText = slidersText [7];

    }

    private void Start()
    {
        settings = (GameSettings)ScriptableObject.CreateInstance("GameSettings");

        InitialisePlayerControls();
        
    }

    /**
     * Prints out all the fields of a settings struct.
     * Debugging purposes only.
     */
    void DebugPrintSettings()
    {
        string output = "";
        foreach (int i in settings.IDtoInput.Keys)
        {
            InputType val;
            settings.IDtoInput.TryGetValue(i, out val);
            output += "\nID: " + i + " Input type: " + val.ToString();
        }
        output += "\nNumber of players: " + settings.playerCount;
        output += "\nGameMode: " + settings.mode.ToString();
        output += "\nNumber of rounds: " + settings.numberRounds;
        output += "\nRound duration: " + settings.roundDuration;
        output += "\nRespawn time: " + settings.respawnTime;
        output += "\nMax lives: " + settings.maxLives;
        output += "\nTarget score: " + settings.targetScore;
        output += "\nTarget kills: " + settings.targetKills;

        Debug.Log(output);
    }


    /**
     * Changes the state of settingup.
     * Used from the menuController when we go to the GameSetup_Panel.
     * This will allow inputs from the players control which team they are assigned to.
     */
    public void SettingUp(bool setup)
    {
        settingUp = setup;
    }

    void Update() {
        //if (!settingUp) return;

        //TODO take all players' inputs and use it to determine their team
        //When a player presses boost it will lock them to that team
        // Pressing 'activate' will cancel and let them move again

        // NOTE will have to make only player1 able to navigate the menu else it will probably get messy
    }

    /**
     * Assigns the initial control inputs to the players
     */
    void InitialisePlayerControls()
    {
        settings.IDtoInput = new Dictionary<int, InputType>();
        string[] controllers = Input.GetJoystickNames();
        // There are always at least 2 players, keyboard and mouse, the rest are controllers
        settings.playerCount = Mathf.Clamp((controllers.Length), 0, 4) + 2;
        // Add all the players into the ID to Input dictionary
        settings.IDtoInput.Add(0, InputType.Keyboard);
        settings.IDtoInput.Add(1, InputType.Mouse);
        for (int i = 2; i < settings.playerCount; i++)
        {
            settings.IDtoInput.Add(i, InputType.Controller);
        }
    }

	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        // Do nothing if we dont load the game scene
        if (scene != SceneManager.GetSceneByName("GameScene")) return;

		SceneManager.MoveGameObjectToScene (gameObject, SceneManager.GetSceneByName("GameScene"));
		NewGame ();
	}

    public void NewGame() {
		
        // Get the game controller
        controller = GameController.instance.gameObject;
        control = controller.GetComponent<GameController>();
        generator = controller.GetComponent<ArenaGenerator>();
        generator.Generate();
        control.CrackedCenterSetup();
        control.SetGameSettings(settings);
	}



    /*== Setter functions for the UI to alter values ==*/
	// (Mathf.CeilToInt(sliderValue/interval) * interval).ToString();

	public void SetRoundQuantity(float quantity)
	{
		float interval = 1f;
		settings.numberRounds = (int)(Mathf.CeilToInt(quantity/interval) * interval);
		roundsText.text = "Number of Rounds: " + settings.numberRounds;
	}

    public void SetRoundDuration(float duration)
    {
		float interval = 10f;
        if (duration == 0.0f)
        {
            settings.roundDuration = float.PositiveInfinity;
        }
        else
        {
			settings.roundDuration = (Mathf.CeilToInt(duration/interval) * interval);
        }

		durationText.text = "Round Duration: " + settings.roundDuration;
    }

    public void SetMaxLives(float lives)
    {
		float interval = 1f;
		settings.maxLives = (int)(Mathf.CeilToInt(lives/interval) * interval);
		livesText.text = "Maximum Lives: " + settings.maxLives;
    }

    public void SetTargetScore(float score)
    {
		float interval = 100f;
		settings.targetScore = (int)(Mathf.CeilToInt(score/interval) * interval);
		targetScoreText.text = "Target Score: " + settings.targetScore;
    }

    public void SetTargetKills(float kills)
    {
		float interval = 2f;
		settings.targetKills = (int)(Mathf.CeilToInt(kills/interval) * interval);
		targetKillsText.text = "Target Demotions: " + settings.targetKills;
    }

    public void SetRespawnTime(float respawnTime)
    {
		float interval = 1f;
		settings.respawnTime = (Mathf.CeilToInt (respawnTime / interval) * interval);
		respawnText.text = "Respawn Time: " + settings.respawnTime;
    }

    public void SetGameMode(int modeIndex)
    {
        settings.mode = (GameMode)modeIndex;
    }
}
