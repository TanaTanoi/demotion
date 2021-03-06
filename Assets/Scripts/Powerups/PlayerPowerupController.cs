﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Powerup;

public class PlayerPowerupController : MonoBehaviour {

	private PlayerMovement playerMovement;
	private PlayerParticleController particleController;
	private PlayerStats stats;

	public FMODUnity.StudioEventEmitter shieldSound;
	public FMODUnity.StudioEventEmitter bananaSound;
	public FMODUnity.StudioEventEmitter snowballSound;
	public FMODUnity.StudioEventEmitter coffeeSound;

	Dictionary<Powerup.Type, float> PowerupEndtimes = new Dictionary<Powerup.Type, float>();

    void Start() {
        particleController = GetComponentInChildren<PlayerParticleController>();
        playerMovement = GetComponentInParent<PlayerMovement>();
		stats = playerMovement.stats;
    }

	void Update () {
		if (PowerupEndtimes.Count > 0) {
			CheckPowerups ();
		}
	}

	public float TimeForPowerup(Powerup.Type type){
		return PowerupEndtimes [type];
	}

	// On collision with a powerup, add it
	void OnTriggerEnter(Collider other) {
		PowerupController powerup = other.gameObject.GetComponent<PowerupController>();
		if ( powerup != null) {
			ActivatePowerup (powerup.type);
			powerup.Pickup (gameObject);
		}

	}

	// Shield active ability occurs here
	void OnTriggerStay(Collider other){
		if (ShieldActive () && !other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Powerup")) {
			Rigidbody rb = other.gameObject.GetComponent<Rigidbody> ();
			if (rb != null) {
				Vector3 dir = (other.transform.position - transform.position);
				float power = stats.SHIELD_PUSH_POWER * (1 / dir.magnitude);
				rb.AddForce (dir.normalized * power);
			}
		}
	}

	// Checks if any powerups are expired, then removes them
	private void CheckPowerups() {
		// Put in a set to prevent removing while iterating
		HashSet<Type> toRemove = new HashSet<Type> ();
		foreach(KeyValuePair<Type, float> pair in PowerupEndtimes) {
			if (pair.Value - Time.time < 0.0f) {
				toRemove.Add (pair.Key);
			} else if(pair.Value - Time.time < FlashTimeForPowerup(pair.Key)){
				particleController.setPowerupNotifyFlash (pair.Key);
			}
		}
		foreach (Type type in toRemove) {
			EndPowerup (type);
		}
	}

	private float FlashTimeForPowerup(Powerup.Type type){
		// this method kinda sucks. Ideally it's a fixed time apart from a few special cases where they are short durations?
		switch (type) {
		case Type.STICKY:
			return stats.STICKY_DURATION / 4;
		case Type.BOOST:
			return stats.BOOST_DURATION / 2;
		case Type.POWER:
			return stats.BOOST_DURATION / 4;
		case Type.BANANA:
			return stats.BANANA_POWER / 2;
		case Type.SHIELD:
			return stats.SHIELD_DURATION / 4f;
		default:
			return 0;
		}
	}

	// Remove the powerup from our counter and reset it's effects
	private void EndPowerup(Powerup.Type type) {
		// Disable particle effects
		particleController.SetEffectActive (type, false);

		// Remove gameplay effects
		PowerupEndtimes.Remove (type);

		switch (type) {
		case Type.BOOST:
			playerMovement.setBoostCooldown (stats.DEFAULT_COOLDOWN);
			break;
		case Type.POWER:
			playerMovement.setBoostPower (stats.DEFAULT_BOOST_POWER);
			break;
		case Type.STICKY:
			playerMovement.SetRotationSpeed (stats.DEFAULT_ROTATION_SPEED);
			break;
		case Type.BANANA:
			playerMovement.SetRotationSpeed (stats.DEFAULT_ROTATION_SPEED);
			playerMovement.GetComponent<Rigidbody> ().angularDrag = 10;
			break;
		case Type.SHIELD:
			break;
		}
	}

	// Enable the effects of a particular powerup
	private void ActivatePowerup(Type type) {
		// Enable particle effects for this powerup
		particleController.SetEffectActive (type, true);
		// Enable gameplay effects
		switch (type) {
		case Type.BOOST:
			AddPowerupTime (Type.BOOST, stats.BOOST_DURATION);
			playerMovement.setBoostCooldown (stats.BOOST_POWERUP_COOLDOWN);
			break;
		case Type.POWER:
			coffeeSound.Play ();
			RefreshPowerupTime (type, stats.POWER_DURATION);
			playerMovement.setBoostPower (playerMovement.getBoostPower () + stats.POWER_DELTA);
			break;
		case Type.STICKY:
			snowballSound.Play ();
			AddPowerupTime (Type.STICKY, stats.STICKY_DURATION);
			playerMovement.SetRotationSpeed (stats.STICKY_POWERDOWN_ROTATION_SPEED);
			break;
		case Type.BANANA:
			bananaSound.Play ();
			playerMovement.SetRotationSpeed (0);
			RefreshPowerupTime (type, stats.BANANA_POWER / 100f);
			playerMovement.SpinPlayer (stats.BANANA_POWER);
			Rigidbody rb = playerMovement.GetComponent<Rigidbody> ();
			rb.AddForce (rb.velocity * -0.95f);
			break;
		case Type.SHIELD:
			shieldSound.Play ();
			AddPowerupTime (type, stats.SHIELD_DURATION);
			break;
		}
	}

	private bool ShieldActive(){
		return PowerupEndtimes.ContainsKey(Powerup.Type.SHIELD);
	}


	// Add additional time to a powerup (e.g. picking up two 5s will give 10s)
	private void AddPowerupTime(Type type, float duration) {
		if (PowerupEndtimes.ContainsKey (type)) {
			PowerupEndtimes [type] = PowerupEndtimes [type] + duration;
		} else {
			PowerupEndtimes [type] = Time.time + duration;
		}
	}

	// Refresh the time of a powerup (e.g. picking up two 5s will give 5s)
	private void RefreshPowerupTime(Type type, float duration) {
		PowerupEndtimes [type] = Time.time + duration;
	}
}

namespace Powerup { 
	// The enum is declared here (as opposed to PowerupController)
	// Because it is used more often here
    // (Count is not a powerup, simply used to define the number of item there are (e.g. PowerupPowerup.Type.Count))
	public enum Type { BOOST, POWER, STICKY, BANANA, SHIELD, Count };
}