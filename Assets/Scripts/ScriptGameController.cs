using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptGameController : MonoBehaviour {

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
	}
}
