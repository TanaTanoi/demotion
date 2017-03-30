using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupController : MonoBehaviour {

	enum PowerupType { BOOST };

	public PlayerMovement Player;

	Dictionary<PowerupType, float> PowerupEndtimes = new Dictionary<PowerupType, float>();

	private const float BOOST_DURATION = 4.0f;
	private const float BOOST_POWERUP_COOLDOWN = 0.1f;

	// Use this for initialization
	void Start () {
		// Set default times of powerup types
		PowerupEndtimes[PowerupType.BOOST] = 0.0f;
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckPowerups ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Powerup")) {
			ActivatePowerup (PowerupType.BOOST);
			Destroy (other.gameObject);
		}
	}

	private void CheckPowerups() {
		foreach(KeyValuePair<PowerupType, float> pair in PowerupEndtimes) {
			if (pair.Value - Time.time < 0.0f) {
				EndPowerup (pair.Key);
			}
		}
	}

	private void EndPowerup(PowerupType type) {
		switch (type) {
		case PowerupType.BOOST:
			Player.setBoostCooldown (PlayerMovement.DEFAULT_COOLDOWN);
			break;
		default:
			break;
		}
	}

	private void ActivatePowerup(PowerupType type) {
		switch (type) {
		case PowerupType.BOOST:
			AddPowerup (PowerupType.BOOST, BOOST_DURATION);
			Player.setBoostCooldown (BOOST_POWERUP_COOLDOWN);
			break;
		default:
			Debug.Log ("wat");
			break;
		}
	}

	private void AddPowerup(PowerupType type, float duration) {
		float newTime = Mathf.Max (
			PowerupEndtimes [type] + duration,
			Time.time + duration
		);
		PowerupEndtimes [type] = newTime;
	}
}
