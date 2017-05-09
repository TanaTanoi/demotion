using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupParticleController : MonoBehaviour {

	public ParticleSystem boostPSystem;
	public ParticleSystem powerPSystem;

	// Enable or disable a particle system for a particular powerup
	public void setParticleSystemEnabled(Powerup.Type type, bool setEnabled) {
		ParticleSystem pSystem = null;
	
		switch (type) {
		case Powerup.Type.BOOST:
			pSystem = boostPSystem;
			break;
		case Powerup.Type.POWER:
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
