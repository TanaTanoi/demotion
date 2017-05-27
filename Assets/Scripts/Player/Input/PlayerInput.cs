using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour
{

    // Default names for the inputs the players can make to control their characters
    public string horizontal = "Horizontal_KB",
                  vertical = "Vertical_KB",
                  boost = "Boost_KB",
                  activate = "Activate_KB";

    public abstract void turn(float rotationSpeed, float horizontalInput, float verticalInput);



}