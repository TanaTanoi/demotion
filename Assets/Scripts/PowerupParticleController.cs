using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupParticleController : MonoBehaviour {

	public ParticleSystem boostPSystem;
	public ParticleSystem powerPSystem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Enable or disable a particle system for a particular powerup
	public void setParticleSystemEnabled(PlayerPowerupController.PowerupType type, bool setEnabled) {
		ParticleSystem pSystem = null;
	
		switch (type) {
		case PlayerPowerupController.PowerupType.BOOST:
			pSystem = boostPSystem;
			break;
		case PlayerPowerupController.PowerupType.POWER:
			pSystem = powerPSystem;
			break;
		}

		if (setEnabled) {
			pSystem.Play();
		} else {
			pSystem.Stop ();
		}

	}
}
