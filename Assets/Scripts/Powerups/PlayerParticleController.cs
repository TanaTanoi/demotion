using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour {

	public ParticleSystem boostPSystem;
	public ParticleSystem powerPSystem;

    public ParticleSystem superboostPSystem;

	// Enable or disable a particle system for a particular powerup
	public void setPowerupParticleSystemEnabled(Powerup.Type type, bool setEnabled) {
		ParticleSystem pSystem = null;
	
		switch (type) {
		case Powerup.Type.BOOST:
			pSystem = boostPSystem;
			break;
		case Powerup.Type.POWER:
			pSystem = powerPSystem;
			break;
		case Powerup.Type.STICKY:
			pSystem = null;
			break;
		}
		if (pSystem == null) {
			return;
		}
			
		if (setEnabled) {
			pSystem.Play();
		} else {
			pSystem.Stop ();
		}
	}

    public void playItemParticleSystem(Item.Type type) {
        switch (type) {
            case Item.Type.THROWABLE:
				// no particle system
                break;
            case Item.Type.SUPER_BOOST:
                superboostPSystem.Play();
                break;
        }

    }
        
}
