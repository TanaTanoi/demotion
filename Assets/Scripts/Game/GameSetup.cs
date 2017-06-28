using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour {
    private static GameSetup instance = null;
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
    public GameSettings settings;  // Local settings applied via menu
	private ArenaGenerator generator;  // Arena generator, could this be moved to the game controller?
    private bool settingUp = false;  // Setting up boolean so players can set teams
	private SkinIndexs[] skins = null;

    // Use this for initialization
    void Awake() {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

		Text[] slidersText = settingsPanel.GetComponentsInChildren<Text> (true);
		durationText = slidersText [0];
		respawnText = slidersText [1];
		targetScoreText = slidersText [2];
		InitialisePlayers();
    }
		

    /**
     * Assigns the initial control inputs to the players
     */
    void InitialisePlayers()
    {

        settings.players = new List<PlayerSettings>();
        skins = new SkinIndexs[4];
		for (int i = 0; i < 4; i++) {
			skins [i] = new SkinIndexs (0, 0, 0);

		}

        int c = 0;  // controller number
        string[] controllers = Input.GetJoystickNames();

        // There are always 3 keyboard players, the fourth only exists if there are one or more controllers
		settings.playerCount = 3 + Mathf.Min(controllers.Length, 1);

		// Add all player settings to player settings list
		for (int i = 0; i < 3; i++) {
			settings.players.Add(new PlayerSettings(InputType.Keyboard, i, i+1, skins[i]));
		}
        
        for (int i = 3; i < settings.playerCount; i++)
        {
            // Ensure we're adding a valid controller
            while (controllers[c++] == null) ;
			settings.players.Add(new PlayerSettings(InputType.Controller, i, c, skins[i]));
        }

    }

	public void ApplyCustomisation(){
		// input id's
		int kid = 1;
		int cid = 1;

		for(int i = 0; i < settings.playerCount; ++i) {
			// Apply skins
			settings.players [i].indices = skins [i];
			// Apply inputs
			switch (settings.players [i].input) {
			case InputType.Keyboard:
				settings.players [i].keyboardID = kid++;
				settings.players [i].controllerID = -1;
				break;
			case InputType.Controller:
				settings.players [i].controllerID = cid++;
				settings.players [i].keyboardID = -1;
				break;
			}

		}
	}

	public void PopulateSkin(int playerNumber, SkinIndexs indices){
		skins [playerNumber] = indices;
	}

	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		Time.timeScale = 1;
		if (scene == SceneManager.GetSceneByName ("GameScene")) {
			NewGame ();
		}
	}

    public void NewGame() {
		Debug.Log ("A new Game has been called");
        // Get the game controller
        controller = GameController.instance.gameObject;
        control = controller.GetComponent<GameController>();
        generator = controller.GetComponent<ArenaGenerator>();
        generator.Generate();
        control.CrackedCenterSetup();
		control.SetGameSettings(this, settings, GetComponent<PlayerSkins>());
	}

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
        //Its time to go home
#endif
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
		float interval = 1f;
		settings.targetScore = (int)(Mathf.CeilToInt(score/interval) * interval);
		targetScoreText.text = "Target Score: " + settings.targetScore;
    }

    public void SetTargetKills(float kills)
    {
		float interval = 1f;
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


    public GameSettings GetGameSettings()
    {
        return settings;
    }

	/**
	 * Changes the input type for the given player, based off the button given.
	 */
	public void ChangeInputType(ControlButtonToggle playerInputButton) {
		int playerNumber = playerInputButton.playerNumber;
		settings.players [playerNumber].input = playerInputButton.input;
	}
		
}
