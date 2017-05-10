using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Powerup;

public class PlayerPowerupController : MonoBehaviour {

	public PlayerMovement playerMovement;
	public PowerupParticleController particleController;

	Dictionary<Powerup.Type, float> PowerupEndtimes = new Dictionary<Powerup.Type, float>();

	// Decreases boost cooldown. Does not stack, extends duration
	private const float BOOST_DURATION = 2.0f;
	private const float BOOST_POWERUP_COOLDOWN = 0.1f;

	// Increases the boost power. Stacks and refreshes duration
	private const float POWER_DELTA = 400.0f;
	private const float POWER_DURATION = 10.0f;

	void Update () {
		if (PowerupEndtimes.Count > 0) {
			CheckPowerups ();
		}
	}

	// On collision with a powerup, add it
	void OnTriggerEnter(Collider other) {
		PowerupController powerup = other.gameObject.GetComponent<PowerupController>();
		if ( powerup != null) {
			ActivatePowerup (powerup.type);
			powerup.Pickup (gameObject);
		}
	}

	// Checks if any powerups are expired, then removes them
	private void CheckPowerups() {
		HashSet<Powerup.Type> toRemove = new HashSet<Powerup.Type> ();
		foreach(KeyValuePair<Powerup.Type, float> pair in PowerupEndtimes) {
			if (pair.Value - Time.time < 0.0f) {
				toRemove.Add (pair.Key);
			}
		}
		foreach (Powerup.Type type in toRemove) {
			EndPowerup (type);
		}
	}

	// Remove the powerup from our counter and reset it's effects
	private void EndPowerup(Powerup.Type type) {
		// Disable particle effects
		particleController.setParticleSystemEnabled (type, false);

		// Remove gameplay effects
		PowerupEndtimes.Remove (type);

		switch (type) {
		case Powerup.Type.BOOST:
			playerMovement.setBoostCooldown (playerMovement.DEFAULT_COOLDOWN);
			break;
		case Powerup.Type.POWER:
			playerMovement.setBoostPower (playerMovement.DEFAULT_BOOST_POWER);
			break;
		}
	}

	// Enable the effects of a particular powerup
	private void ActivatePowerup(Powerup.Type type) {
		// Enable particle effects for this powerup
		particleController.setParticleSystemEnabled (type, true);

		// Enable gameplay effects
		switch (type) {
		case Powerup.Type.BOOST:
			AddPowerupTime (Powerup.Type.BOOST, BOOST_DURATION);
			playerMovement.setBoostCooldown (BOOST_POWERUP_COOLDOWN);
			break;
		case Powerup.Type.POWER:
			RefreshPowerupTime (type, POWER_DURATION);
			playerMovement.setBoostPower (playerMovement.getBoostPower () + POWER_DELTA);
			break;
		}
	}

	// Add additional time to a powerup (e.g. picking up two 5s will give 10s)
	private void AddPowerupTime(Powerup.Type type, float duration) {
		if (PowerupEndtimes.ContainsKey (type)) {
			PowerupEndtimes [type] = PowerupEndtimes [type] + duration;
		} else {
			PowerupEndtimes [type] = Time.time + duration;
		}
	}

	// Refresh the time of a powerup (e.g. picking up two 5s will give 5s)
	private void RefreshPowerupTime(Powerup.Type type, float duration) {
		PowerupEndtimes [type] = Time.time + duration;
	}
}

namespace Powerup { 
	// The enum is declared here (as opposed to PowerupController)
	// Because it is used more often here
    // (Count is not a powerup, simply used to define the number of item there are (e.g. PowerupPowerup.Type.Count))
	public enum Type { BOOST, POWER, Count };
}