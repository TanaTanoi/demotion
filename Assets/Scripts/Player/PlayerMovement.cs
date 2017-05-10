using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	private float boostCooldown;
    private float boostPower;
    private float rotationSpeed;
    
	private float nextBoostTime = 0.0f;
    private Vector3 aimDirection = Vector3.forward;
    private Vector3 desiredDirection;

    private ModelController playerAnimator;
    private Rigidbody chairRigidbody;

    public InputType inputType = InputType.Keyboard;
    private PlayerInput playerIn;

    // Use this for initialization
    void Start () {
		boostCooldown = PlayerStats.DEFAULT_COOLDOWN;
        boostPower = PlayerStats.DEFAULT_BOOST_POWER;
        rotationSpeed = PlayerStats.DEFAULT_ROTATION_SPEED * 10;

        playerAnimator = GetComponentInChildren<ModelController>();
        chairRigidbody = GetComponent<Rigidbody>();
        chairRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Ensures locked to 2D 
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

        playerIn.turn(rotationSpeed, horizontalInput, verticalInput);

        //desiredDirection = Vector3.Normalize(new Vector3(horizontalInput, 0f, verticalInput));
        if (Input.GetAxisRaw(playerIn.boost) != 0)
        {
            if (nextBoostTime <= Time.time)
            {
                Boost(boostPower);
                nextBoostTime = Time.time + boostCooldown;
            }
        }
    }

    public void Boost(float power)
    {
        playerAnimator.Push();
        chairRigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, power));
    }

    public PlayerInput GetPlayerInput() {
        return playerIn;
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
