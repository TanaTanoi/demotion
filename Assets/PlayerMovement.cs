using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private Vector3 aimDirection = new Vector3 (0, 0, 1);
	private Vector3 desiredDirection;

	public Rigidbody MovementSphere;
	public Transform DirectionObject;

	public float BOOST_POWER = 250;
	public float ROTATION_SPEED = 0.09f;
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		float hoz = Input.GetAxis ("Horizontal");
		float vert = Input.GetAxis ("Vertical");
		desiredDirection = Vector3.Normalize(new Vector3(hoz, 0f ,vert));

		moveTowardsDesiredDirection ();

		bool boosting = Input.GetButtonDown ("Fire1");

		if (boosting) {
			MovementSphere.AddForce (aimDirection * BOOST_POWER);
		}
	}



	void moveTowardsDesiredDirection(){
		aimDirection = Vector3.RotateTowards (aimDirection, desiredDirection, ROTATION_SPEED, 0f);
//		float newY = Mathf.Atan2 (aimDirection.x, aimDirection.z);

//		DirectionObject.rotation = Quaternion.Euler (new Vector3 (DirectionObject.rotation.x, newY, DirectionObject.rotation.z));
		DirectionObject.rotation = Quaternion.LookRotation (aimDirection, new Vector3 (0, 1, 0));
		DirectionObject.position = MovementSphere.transform.position;
	}
}
