using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FanWindScript : MonoBehaviour {

    public float windForce = 3f;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

    private void OnTriggerStay(Collider other)
    {
        Rigidbody otherRB = other.GetComponentInParent<Rigidbody>();
        otherRB.AddForceAtPosition(new Vector3(0,0,-windForce), otherRB.centerOfMass);
        //Debug.Log("Windy here!");
    }
}
