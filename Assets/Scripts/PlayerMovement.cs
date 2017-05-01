using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float DEFAULT_BOOST_POWER = 250;
	public float DEFAULT_ROTATION_SPEED = 0.09f;
	public float DEFAULT_COOLDOWN = 0.7f;

    public float deadzone = 0.25f;  // Deadzone for controller analog stick

	private float boostCooldown;
    private float boostPower;
    private float rotationSpeed;

	private float timestamp = 0.0f;
    private Vector3 aimDirection = Vector3.forward;

    private Rigidbody chairRigidbody;
    private tmpPickupController pickupControl;
    private Animator animator;

    // Use this for initialization
    void Start () {
		boostCooldown = DEFAULT_COOLDOWN;
        boostPower = DEFAULT_BOOST_POWER;
        rotationSpeed = DEFAULT_ROTATION_SPEED;

        animator = GetComponentInChildren<Animator>();
        chairRigidbody = GetComponent<Rigidbody>();
        pickupControl = FindObjectOfType<tmpPickupController>();
    }
	
	// Update is called once per frame
	void Update () {
        
		float horizontalInput = Input.GetAxisRaw ("Horizontal_KB");
        float verticalInput = Input.GetAxisRaw ("Vertical_KB");
        float activeInput = Input.GetAxisRaw("Active_KB");

        if(activeInput != 0)
        {
            Debug.Log("Throw!");
            pickupControl.ThrowItem();
        }

        Vector3 stickInput = new Vector3(horizontalInput, 0f, verticalInput);
        if (stickInput.magnitude < deadzone)
            stickInput = Vector3.zero;
        stickInput = Vector3.Normalize(stickInput);

        //pointDirection(stickInput);
        rotateDirection(horizontalInput);

        float speed = Input.GetAxisRaw("Boost_KB");
        animator.SetFloat("speed", speed);
		if (speed > 0) {
            if (timestamp <= Time.time) {
                animator.SetFloat("speed", speed);
                chairRigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, boostPower));
                timestamp = Time.time + boostCooldown;
            }
		}
        //Lock x and z rotation
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
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

    void rotateDirection(float direction)
    {
        transform.Rotate(new Vector3(0, direction * rotationSpeed, 0), Space.World);
    }

	void pointDirection(Vector3 desiredDirection){
		aimDirection = Vector3.RotateTowards (aimDirection, desiredDirection, rotationSpeed, 0f);
		transform.rotation = Quaternion.LookRotation (aimDirection, new Vector3 (0, 1, 0));
	}
}
