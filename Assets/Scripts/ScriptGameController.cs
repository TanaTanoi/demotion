﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptGameController : MonoBehaviour {

    private bool isPaused = false;


	// Use this for initialization
	void Awake () {
        Debug.Log(Application.platform);
        switch(Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
                //Change to windows controls
                break;
            case RuntimePlatform.LinuxPlayer:
                //Change to linux controls
                break;
            case RuntimePlatform.OSXPlayer:
                //Change to OSX controlss
                break;
        }
        //Set default input controls based on operating system game is runnning on.
	}
	
	// Update is called once per frame
	void Update () {
        //Control game timer / scores, etc

        // Pause
        if(Input.GetAxisRaw("Start") == 1)
        {
            isPaused = !isPaused;   // Toggle paused state
            if (isPaused)
            {
                // Stop time
                Time.timeScale = 0;
                // Open the pause menu here
            }
            else
            {
                // Start time
                Time.timeScale = 1;
            }
        }
	}

    private void OnGUI()
    {
        // Open 
    }

}
