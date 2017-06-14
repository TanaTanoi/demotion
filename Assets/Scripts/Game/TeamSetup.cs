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
        inputs = new Dictionary<int, PlayerInput>();

        InitialiseInputs();
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
//        foreach(PlayerSettings p in players)
//        {
//            switch(p.input)
//            {
//			case InputType.Keyboard:
//				inputs.Add (p.playerID, gameObject.AddComponent<InputKeyboard> () as InputKeyboard);
//					(inputs[p.playerID] as InputKeyboard).RefreshInputs(p.keyboardID);
//                    break;
//                case InputType.Mouse:
//                    inputs.Add(p.playerID, gameObject.AddComponent<InputMouse>() as InputMouse);
//                    break;
//                case InputType.Controller:
//                    inputs.Add(p.playerID, gameObject.AddComponent<InputController>() as InputController);
//					(inputs[p.playerID] as InputController).RefreshInputs(p.controllerID);
//                    break;
//            }
//        }

    }
}
