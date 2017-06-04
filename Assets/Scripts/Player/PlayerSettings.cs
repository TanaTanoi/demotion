using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Settings for this player
 */
public class PlayerSettings : MonoBehaviour {

    public InputType input;
    public int playerID;
    public int teamID;
    //private PlayerPref customisation; // customisation settings

    public PlayerSettings(InputType input, int pid, int tid)
    {
        this.input = input;
        this.playerID = pid;
        this.teamID = tid;
    }


}
