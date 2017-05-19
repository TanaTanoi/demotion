using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : PlayerInput {
    private static int nextID;
    private int id;

    public InputController()
    {
        id = nextID++;
    }

    private void Start()
    {
        horizontal = string.Format("Horizontal_C%d", id);
        vertical = string.Format("Vertical_C%d", id);
        boost = string.Format("Boost_C%d", id);
        activate = string.Format("Activate_C%d", id);
    }

    public override void turn(float rotationSpeed, float horizontalInput, float verticalInput)
    {
        if (horizontalInput + verticalInput == 0) return; // Do nothing if there is no input
        Vector3 targetDirection = new Vector3(horizontalInput, 0f, verticalInput);

        Quaternion newRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

    }
}
