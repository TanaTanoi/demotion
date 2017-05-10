using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float DEFAULT_BOOST_POWER = 450;
	public float DEFAULT_ROTATION_SPEED = 3f;
	public float DEFAULT_COOLDOWN = 0.7f;
    public Animator playerAnimator;

	private float boostCooldown;
    private float boostPower;
    private float rotationSpeed;

	private float timestamp = 0.0f;
    private Vector3 aimDirection = Vector3.forward;
    private Vector3 desiredDirection;

    private Rigidbody chairRigidbody;

    public InputType inputType = InputType.Keyboard;
    private PlayerInput playerIn;

    // Use this for initialization
    void Awake () {
		boostCooldown = DEFAULT_COOLDOWN;
        boostPower = DEFAULT_BOOST_POWER;
        rotationSpeed = DEFAULT_ROTATION_SPEED;

        chairRigidbody = GetComponent<Rigidbody>();
        switch(inputType)
        {
            case InputType.Controller:
                playerIn = gameObject.AddComponent<InputController>() as InputController;
                break;
            case InputType.Keyboard:
                playerIn = gameObject.AddComponent <InputKeyboard>() as InputKeyboard;
                break;
            case InputType.Mouse:
                playerIn = gameObject.AddComponent <InputMouse>() as InputMouse;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis(playerIn.horizontal);
        float verticalInput = Input.GetAxis(playerIn.vertical);

        //if(horizontalInput != 0 || verticalInput != 0) {
        playerIn.turn(rotationSpeed*horizontalInput);

        //desiredDirection = Vector3.Normalize(new Vector3(horizontalInput, 0f, verticalInput));
        if (Input.GetAxisRaw(playerIn.boost) != 0)
        {
            Boost();
        }
    }

    void Boost()
    {
        if (timestamp <= Time.time)
        {
            playerAnimator.SetTrigger("Push");
            chairRigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, boostPower));
            timestamp = Time.time + boostCooldown;
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


	void RotateController()
    {
		aimDirection = Vector3.RotateTowards (aimDirection, desiredDirection, rotationSpeed, 0f);
		transform.rotation = Quaternion.LookRotation (aimDirection, new Vector3 (0, 1, 0));
	}

}
