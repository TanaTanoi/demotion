﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : PlayerInput {

    int id = -1;  // Controller id

    public override void turn(float rotationSpeed, float horizontalInput, float verticalInput)
    {
        if (id == -1) Debug.Log("ID is -1, ensure this is set before gameplay!");

        if (horizontalInput + verticalInput == 0) return; // Do nothing if there is no input
        Vector3 targetDirection = new Vector3(horizontalInput, 0f, verticalInput);

        Quaternion newRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.fixedDeltaTime);

    }

    public void RefreshInputs(int id)
    {
        // Don't need to update anything if the id's are the same
        if (this.id == id) return;
        this.id = id;
        horizontal = string.Format("Horizontal_C{0}", id);
        vertical = string.Format("Vertical_C{0}", id);
        boost = string.Format("Boost_C{0}", id);
        activate = string.Format("Activate_C{0}", id);
    }
}
