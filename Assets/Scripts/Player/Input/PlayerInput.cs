using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour
{

    // Base names for the inputs the players can make to control their characters
    public string horizontal = "Horizontal_KB",
                  vertical = "Vertical_KB",
                  boost = "Boost_KB",
                  activate = "Activate_KB";

    public PlayerNumber playerNumber;

    public PlayerInput(PlayerNumber pNum)
    {
        playerNumber = pNum;
    }
    
   

    public abstract void turn(float rotationSpeed);
    

}