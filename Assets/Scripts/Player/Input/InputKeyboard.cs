using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : PlayerInput {

    void Start()
    {
        horizontal = "Horizontal_KB";
        vertical = "Vertical_KB";
        boost = "Boost_KB";
        activate = "Activate_KB";
    }

    public override void turn(float rotationSpeed)
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0), Space.World);
        
    }
}
