using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Settings for this player
 */
public class PlayerSettings {

    public InputType input;
    public int controllerID;
	public int keyboardID;
    public int playerID;
	public SkinIndexs indices;

	public PlayerSettings(InputType input, int pid, int inputid, SkinIndexs indices)
    {
		if (input == InputType.Controller) {
			this.controllerID = inputid;
			this.keyboardID = -1;
		} else if (input == InputType.Keyboard) {
			this.keyboardID = inputid;
			this.controllerID = -1;
		}

        this.input = input;
        this.playerID = pid;
		this.indices = indices;
    }
}
