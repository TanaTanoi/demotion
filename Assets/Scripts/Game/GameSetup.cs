using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {
    //Game type enums
    public enum GameMode { DEATHMATCH, HIGHSCORE };

    private GameController control;

    // Player settings
    private Dictionary<int, InputType> playerIDtoInputDict;  // Dictionary between player ID number (p1,p2,etc) and their control method
    private int playerCount;

    // Gamemode settings
    private GameMode mode;
    private int numberRounds;
    private float roundDuration;
    private int maxLives;
    private int maxScore;

    // Use this for initialization
    void Awake () {
        //Don't destroy this
        DontDestroyOnLoad(gameObject);
	}
	
	
	void Update () {
		
	}

    /**
     * Assigns the initial control inputs to the players
     */
    void InitialisePlayerControls()
    {
        string[] controllers = Input.GetJoystickNames();
        // There are always at least 2 players, keyboard and mouse, the rest are controllers
        playerCount = Mathf.Clamp((controllers.Length), 0, 4) + 2;
        // Add all the players into the ID to Input dictionary
        playerIDtoInputDict.Add(0, InputType.Keyboard);
        playerIDtoInputDict.Add(1, InputType.Mouse);
        for (int i = 2; i < playerCount; i++)
        {
            playerIDtoInputDict.Add(i, InputType.Controller);
        }
    }

    /**
     * Creates a new GameController and assigns the gamemode and settings
     */
    public void NewGame() {
        control = gameObject.AddComponent<GameController>() as GameController;
        control.ApplyGameMode(mode, numberRounds, roundDuration, maxLives, maxScore);
    }
}
