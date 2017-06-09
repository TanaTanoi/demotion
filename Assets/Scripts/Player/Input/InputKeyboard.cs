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

    public override void turn(float rotationSpeed, float horizontalInput, float verticalInput)
    {
        float y = rotationSpeed * horizontalInput;// * Time.fixedDeltaTime; // <- this make the turn speed basically 0
        transform.Rotate(new Vector3(0, y, 0), Space.World);
    }
}
