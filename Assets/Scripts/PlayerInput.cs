using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public enum PlayerNumber {Player1, Player2, Player3, Player4};


    public PlayerNumber playerNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //float horizontalInput = Input.GetAxisRaw("Horizontal_KB");
	}
}
