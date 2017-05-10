using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : PlayerInput {

    public InputMouse(PlayerNumber pNum) : base(pNum) {}

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
    public override void turn(float rotationSpeed)
    {
        Plane aeroplane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        if(aeroplane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
