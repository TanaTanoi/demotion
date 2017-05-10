using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private ModelController playerAnimator;

	private float boostCooldown;
    private float boostPower;
    private float rotationSpeed;

	private float timestamp = 0.0f;
    private Vector3 aimDirection = Vector3.forward;
    private Vector3 desiredDirection;

    private Rigidbody chairRigidbody;
    // Use this for initialization
    void Start () {
		boostCooldown = PlayerStats.DEFAULT_COOLDOWN;
        boostPower = PlayerStats.DEFAULT_BOOST_POWER;
        rotationSpeed = PlayerStats.DEFAULT_ROTATION_SPEED;

        playerAnimator = GetComponentInChildren<ModelController>();
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
                Boost(boostPower);
                timestamp = Time.time + boostCooldown;
            }
		}
	}


    public void Boost(float power) {
        playerAnimator.Push();
        chairRigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, power));
    }

    public float getBoostCooldown()
    {
        return boostCooldown;
    }

	public void setBoostCooldown(float cooldown) {
		boostCooldown = cooldown;
	}

	public float getBoostPower(){
		return boostPower;
	}

	public void setBoostPower(float power) {
		boostPower = power;
	}

	void moveTowardsDesiredDirection(){
		aimDirection = Vector3.RotateTowards (aimDirection, desiredDirection, rotationSpeed, 0f);
		transform.rotation = Quaternion.LookRotation (aimDirection, new Vector3 (0, 1, 0));
		//DirectionObject.position = transform.position;
	}
}
