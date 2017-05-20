using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {
    public enum GameMode { DEATHMATCH, HIGHSCORE };
    private GameController control;

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
	
	// Update is called once per frame
	void Update () {
		
	}

    /**
     * Creates a new GameController and assigns the gamemode and settings
     */
    public void NewGame() {
        control = gameObject.AddComponent<GameController>() as GameController;
        control.ApplyGameMode(mode, numberRounds, roundDuration, maxLives, maxScore);
    }
}
