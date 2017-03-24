using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float DEFAULT_BOOST_POWER = 250;
	public float DEFAULT_ROTATION_SPEED = 0.09f;
	public float DEFAULT_COOLDOWN = 0.7f;

	private float boostCooldown;
    private float boostPower;
    private float rotationSpeed;

	private float timestamp = 0.0f;
    private Vector3 aimDirection = Vector3.forward;
    private Vector3 desiredDirection;

    private Rigidbody chairRigidbody;

    // Use this for initialization
    void Start () {
		boostCooldown = DEFAULT_COOLDOWN;
        boostPower = DEFAULT_BOOST_POWER;
        rotationSpeed = DEFAULT_ROTATION_SPEED;

        chairRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticalInput = Input.GetAxis ("Vertical");

		desiredDirection = Vector3.Normalize(new Vector3(horizontalInput, 0f , verticalInput));

		moveTowardsDesiredDirection ();

		if (Input.GetAxisRaw("Boost") != 0) {
            if (timestamp <= Time.time) {
                chairRigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, boostPower));
                timestamp = Time.time + boostCooldown;
            }
		}
	}


    public float getBoostCooldown()
    {
        return boostCooldown;
    }

	void moveTowardsDesiredDirection(){
		aimDirection = Vector3.RotateTowards (aimDirection, desiredDirection, rotationSpeed, 0f);
		transform.rotation = Quaternion.LookRotation (aimDirection, new Vector3 (0, 1, 0));
		//DirectionObject.position = transform.position;
	}
}
