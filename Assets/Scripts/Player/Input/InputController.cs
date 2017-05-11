using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : PlayerInput {

    private void Start()
    {
        horizontal = "Horizontal_C1"; // Want to add player number, however the input is determined by the joystick number not the player  number
        vertical = "Vertical_C1";
        boost = "Boost_C1";
        activate = "Activate_C1";
    }

    public override void turn(float rotationSpeed, float horizontalInput, float verticalInput)
    {
        if (horizontalInput + verticalInput == 0) return; // Do nothing if there is no input
        Vector3 targetDirection = new Vector3(horizontalInput, 0f, verticalInput);

        Quaternion newRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

    }
}
