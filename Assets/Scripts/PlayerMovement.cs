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
    private Animator animator;

    // Use this for initialization
    void Start () {
		boostCooldown = DEFAULT_COOLDOWN;
        boostPower = DEFAULT_BOOST_POWER;
        rotationSpeed = DEFAULT_ROTATION_SPEED;

        animator = GetComponentInChildren<Animator>();
        chairRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		float horizontalInput = Input.GetAxisRaw ("Horizontal_C1");
        float verticalInput = Input.GetAxisRaw ("Vertical_C1");
        
        Vector3 stickInput = new Vector3(horizontalInput, 0f, verticalInput);
        if (stickInput.magnitude < deadzone)
            stickInput = Vector3.zero;
        stickInput = Vector3.Normalize(stickInput);

        pointDirection(stickInput);
        //rotateDirection(stickInput);

        float speed = Input.GetAxisRaw("Boost_C1");
        animator.SetFloat("speed", speed);
		if (speed > 0) {
            if (timestamp <= Time.time) {
                animator.SetFloat("speed", speed);
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

	void pointDirection(Vector3 desiredDirection){
		aimDirection = Vector3.RotateTowards (aimDirection, desiredDirection, rotationSpeed, 0f);
		transform.rotation = Quaternion.LookRotation (aimDirection, new Vector3 (0, 1, 0));
	}
}
