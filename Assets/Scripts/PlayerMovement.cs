using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public const float DEFAULT_BOOST_POWER = 450;
	public const float DEFAULT_ROTATION_SPEED = 0.09f;
	public const float DEFAULT_COOLDOWN = 0.7f;
    public Animator playerAnimator;

	private float boostCooldown;
    private float boostPower;
    private float rotationSpeed;

	private float timestamp = 0.0f;
    private Vector3 aimDirection = Vector3.forward;
    private Vector3 desiredDirection;

    private Rigidbody chairRigidbody;
    private PlayerInput playerIn;

    // Use this for initialization
    void Start () {
		boostCooldown = DEFAULT_COOLDOWN;
        boostPower = DEFAULT_BOOST_POWER;
        rotationSpeed = DEFAULT_ROTATION_SPEED;

        chairRigidbody = GetComponent<Rigidbody>();
        playerIn = GetComponent<PlayerInput>();
    }
	
	// Update is called once per frame
	void Update () {
		float horizontalInput = Input.GetAxis (playerIn.horizontal);
		float verticalInput = Input.GetAxis (playerIn.vertical);

		desiredDirection = Vector3.Normalize(new Vector3(horizontalInput, 0f , verticalInput));

		moveTowardsDesiredDirection ();

		if (Input.GetAxisRaw(playerIn.boost) != 0) {
            if (timestamp <= Time.time) {
                playerAnimator.SetTrigger("Push");
                chairRigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, boostPower));
                timestamp = Time.time + boostCooldown;
            }
		}
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
