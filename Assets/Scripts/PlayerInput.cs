using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    /**
     * Script will eventutally be used to allow control remapping and customisation per player.
     */
    public enum PlayerNumber {Player1, Player2, Player3, Player4};
    public enum ControlInput {XBox360, Ps3, Ps4, XBoxOne, Steam, Keyboard, Mouse};

    public PlayerNumber playerNumber;
    public ControlInput playerInput;

    public string horizontal = "Horizontal_KB";
    public string vertical = "Vertical_KB";
    public string boost = "Boost_KB";
    public string activate = "Activate_KB";

}
