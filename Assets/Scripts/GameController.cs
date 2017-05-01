﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text timerText;
    public float timeLeft = 99;

    private PlayerInput[] players;

	// Use this for initialization
	void Start () {
        players = FindObjectsOfType<PlayerInput>();
        Debug.Log("There are " + players.Length + " players in this game.");
        timerText.text = "Time: " + (int)timeLeft;
    }

    // Update is called once per frame
    void Update() {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + (int)timeLeft;
    }
}
