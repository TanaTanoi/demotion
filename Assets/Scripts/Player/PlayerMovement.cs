﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public PlayerStats stats;
	public InputType inputType = InputType.Keyboard;
	public int playerNum;


    private float boostCooldown;
    private float boostPower;
	private float rotationSpeed; // If rotation speed is zero, the player can't move
    private float deadZone = 0.25f;

    private float nextBoostTime = 0.0f;

    private PlayerModelController playerAnimator;
    private Rigidbody chairRigidbody;

    private PlayerInput playerIn;

	private float boostHoldDuration = 0f;

	private bool boostDown = false;

    // Use this for initialization
    void Start() {
        boostCooldown = stats.DEFAULT_COOLDOWN;
        boostPower = stats.DEFAULT_BOOST_POWER;
        rotationSpeed = stats.DEFAULT_ROTATION_SPEED;

        playerAnimator = GetComponentInChildren<PlayerModelController>();
        chairRigidbody = GetComponentInChildren<Rigidbody>();
        chairRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Ensures locked to 2D. but why this over the editor?
        //playerIn = gameObject.AddComponent<InputMouse>() as InputMouse; //set to Mouse to start with before change
		//SetInput(inputType);
		SetUpInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis(playerIn.horizontal);
        float verticalInput = Input.GetAxis(playerIn.vertical);

		if (rotationSpeed > 0) {
			//gyroscope back to the correct orientation
			transform.rotation = Quaternion.Euler (0.0f, transform.rotation.eulerAngles.y, 0.0f);
			playerIn.turn (rotationSpeed, horizontalInput, verticalInput);
		

			//desiredDirection = Vector3.Normalize(new Vector3(horizontalInput, 0f, verticalInput));
			if (!boostDown && Input.GetAxisRaw (playerIn.boost) > deadZone) {
				//if (nextBoostTime <= Time.time) {
				boostDown = true;
				boostHoldDuration = Time.time;
				nextBoostTime = Time.time + boostCooldown;
			//	}
			}

			if(boostDown && Input.GetAxisRaw(playerIn.boost) <= deadZone) {
				boostHoldDuration = Mathf.Min ((Time.time - boostHoldDuration), stats.DEFAULT_MAX_BOOST_HOLD_TIME);
				boostHoldDuration = boostHoldDuration / stats.DEFAULT_MAX_BOOST_HOLD_TIME;
				boostHoldDuration = stats.BOOST_POWER_RAMP.Evaluate (boostHoldDuration);
				float boost = boostHoldDuration * boostPower;// + stats.MINIMUM_BOOST_POWER;
				Boost (boost);

				boostDown = false;
			}
		}

    }

    /**
     * Sets the player's input type to the input provided
     */
    public void SetInput(InputType input){
        switch(input)
        {
            case InputType.Keyboard:
                playerIn = gameObject.AddComponent<InputKeyboard>() as InputKeyboard;
                break;
            case InputType.Mouse:
                playerIn = gameObject.AddComponent<InputMouse>() as InputMouse;
                break;
            case InputType.Controller:
                playerIn = gameObject.AddComponent<InputController>() as InputController;
                break;
        }
    }

	// Spins the player some amount
	public void SpinPlayer(float power){
		chairRigidbody.AddForce (Vector3.up * power * 2);
		chairRigidbody.AddRelativeTorque (Vector3.up * power, ForceMode.VelocityChange);
	}
		

    /**
     * Boosts the player forward!
     */
    public void Boost(float power)
    {
		playerAnimator.Push ();
        chairRigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, power));
    }

    public PlayerInput GetPlayerInput() {
        return playerIn;
    }

	public void SetRotationSpeed(float rotationSpeed)
	{
		this.rotationSpeed = rotationSpeed;
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

	public int GetPlayerNum(){
		return this.playerNum;
	}

	public void SetPlayerNum(int playerNum){
		this.playerNum = playerNum;

		//This is here for the demo
		if (playerNum == 1) {
			inputType = InputType.Keyboard;
		} else {
			inputType = InputType.Mouse;
		}
	}

	/**
	 * This mehtod is here for the demo so the plaeyr respawns with the correct player input
	 **/
	private void SetUpInput(){
		if (playerNum == 1) {
			SetInput(InputType.Keyboard);
		} else {
			SetInput(InputType.Mouse);
		}
	}

}
