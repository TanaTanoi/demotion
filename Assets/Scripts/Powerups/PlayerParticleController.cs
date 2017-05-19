using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour {

	public ParticleSystem boostPSystem;
	public ParticleSystem powerPSystem;

	private TrailRenderer trails;

	void Start(){
		trails = GetComponent<TrailRenderer> ();
	}
		

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
            case Item.Type.STICKY_THROWABLE:
				// no particle system
                break;
		case Item.Type.SUPER_BOOST:
			trails.time = 1;
			StartCoroutine ("BoostTrail");
                break;
        }

    }

	IEnumerator BoostTrail() {
		
		while (trails.time > 0) {
			yield return new WaitForSeconds(0.1f);
			trails.time = trails.time - 0.1f;
		}

		trails.time = 0;
	}
        
}
