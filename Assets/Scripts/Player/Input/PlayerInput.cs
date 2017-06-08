using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInput 
{

    // Default names for the inputs the players can make to control their characters
    public string horizontal = "Horizontal_KB1",
                  vertical = "Vertical_KB1",
                  boost = "Boost_KB1",
                  activate = "Activate_KB1";

	public abstract void turn(float rotationSpeed, float horizontalInput, float verticalInput, Transform transform);



}