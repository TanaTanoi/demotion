using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSetup : MonoBehaviour {

    public GameSetup setup;
    private List<PlayerSettings> players;
    private Dictionary<int, PlayerInput> inputs;

	// Use this for initialization
	void Start () {
        if (setup == null) Debug.LogError("GameSetup not attached");

        players = setup.GetGameSettings().players;
	}
	
	// Update is called once per frame
	void Update () {
		// Player 1
        foreach(PlayerSettings p in players)
        {

        }


        // Player 2

        // Player 3

        // Player 4


	}

    private void InitialiseInputs()
    {
        foreach(PlayerSettings p in players)
        {
            //switch
        }

    }
}
