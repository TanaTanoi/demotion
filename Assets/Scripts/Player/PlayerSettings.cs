using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Settings for this player
 */
public class PlayerSettings : MonoBehaviour {

    public InputType input;
    public int controllerID;
    public int playerID;
    public int teamID;
	public SkinIndexs indices;


	public PlayerSettings(InputType input, int pid, int tid, SkinIndexs indices)
    {
        if(input == InputType.Controller)
        {
            throw new System.ArgumentException("Error, using the wrong constructor with InputType; controller.");
        }

        this.input = input;
        this.controllerID = -1;
        this.playerID = pid;
        this.teamID = tid;
    }

	public PlayerSettings(InputType input, int cid, int pid, int tid, SkinIndexs indices)
    {
        if (input != InputType.Controller)
        {
            Debug.Log(input);
            throw new System.ArgumentException("Error, using the controller constructor when InputType is not controller.");
        }

        this.input = input;
        this.controllerID = cid;
        this.playerID = pid;
        this.teamID = tid;
    }


}
