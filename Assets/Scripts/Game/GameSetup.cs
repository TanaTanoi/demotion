﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour {

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
     * Assigns the initial control inputs to the players
     */
    void InitialisePlayerControls()
    {
        settings.players = new List<PlayerSettings>();
        string[] controllers = Input.GetJoystickNames();
        // There are always at least 2 players, keyboard and mouse, the rest are controllers
        settings.playerCount = Mathf.Clamp((controllers.Length), 0, 4) + 2;
        // Add all player settings to player settings list
        int p = 0; // player number, also used as temporary team number
        settings.players.Add(new PlayerSettings(InputType.Keyboard, p, p++));
        settings.players.Add(new PlayerSettings(InputType.Mouse, p, p++));
        for (int i = 2; i < settings.playerCount; i++)
        {
            settings.players.Add(new PlayerSettings(InputType.Controller, i, i));
        }
    }

	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
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

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
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


    public GameSettings GetGameSettings()
    {
        return settings;
    }
}
