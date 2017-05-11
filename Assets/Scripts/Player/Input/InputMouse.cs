using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : PlayerInput {

    public int floorMask;
    public float camRayLength = 100f;

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");

        horizontal = "Horizontal_MS";
        vertical = "Vertical_MS";
        boost = "Boost_MS";
        activate = "Activate_MS";
    }

    // turn towards the mouse
    public override void turn(float rotationSpeed, float horizontalInput, float verticalInput)
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //playerRigidbody.MoveRotation(Quaternion.Slerp();
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed*Time.deltaTime);
        }
    }

}
