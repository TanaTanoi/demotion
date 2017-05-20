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


    private bool settingUp = false;

    // Use this for initialization
    void Awake () {
        //Don't destroy this
        DontDestroyOnLoad(gameObject);
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
	
	void Update () {
        if (!settingUp) return;
        
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
        control.ApplyGameMode(playerIDtoInputDict, mode, numberRounds, roundDuration, maxLives, maxScore);
    }
}
