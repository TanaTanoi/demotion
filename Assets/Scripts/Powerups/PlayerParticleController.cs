using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour {

	[System.Serializable]
	public class Effect {
		public Powerup.Type type;
		public ParticleSystem particles;
		public GameObject prop;
		public EffectStatus status;
		public Effect(Powerup.Type t, ParticleSystem pa, GameObject pr, EffectStatus s){
			type = t;
			particles = pa;
			prop = pr;
			status = s;
		}
	}

	public ParticleSystem superBoostEffects;

	public enum EffectStatus{ HIDDEN, FLASHING, ACTIVE };

	public Effect[] effects;
	private Dictionary<Powerup.Type, GameObject> activeEffectProps = new Dictionary<Powerup.Type, GameObject>();
	private TrailRenderer trails;

	private Vector3 propOffset = new Vector3 (0, 2, 0);
	private Quaternion propRotation = Quaternion.Euler (new Vector3 (-45, -45, 0));

	private Transform camera;

	void Start(){
		trails = GetComponent<TrailRenderer> ();
		camera = GameObject.FindObjectOfType<Camera> ().transform;
	}


	void FixedUpdate(){
		// Offset is relative to camera position to ensure it aligns with the camera, regardless of orientation
		Vector3 offset = -camera.right * ((activeEffectProps.Count - 1) / 2);
		foreach(KeyValuePair<Powerup.Type, GameObject> pair in activeEffectProps){
			// Flash based on sine wave of time
			if (effects.First (x => x.type == pair.Key).status == EffectStatus.FLASHING) {
				int t = Mathf.RoundToInt((Mathf.Sin (Time.time * 10) * 0.5f) + 0.5f);
				pair.Value.GetComponent<Renderer> ().enabled = (t == 0);
			}
			// Align the position based on how many effects they have
			pair.Value.transform.position = transform.position + propOffset + offset;
			offset += camera.right;
		}
	}

	// Enable or disable an effect for a particular de/buff
	public void SetEffectActive(Powerup.Type type, bool setEnabled) {
		// select the first system of this type
		Debug.Log(type);
		Effect effect = null;// = effects.First(x => x.type == type);
		foreach (Effect e in effects) {
			if (e.type == type) {
				effect = e;
			}
		}

		if (effect == null)
			return;

		if (setEnabled) {
			if (effect.status != EffectStatus.ACTIVE) {
				activeEffectProps [effect.type] = Instantiate (effect.prop, transform);
				activeEffectProps [effect.type].transform.localPosition = propOffset;
				activeEffectProps [effect.type].transform.rotation = propRotation;
			}
			effect.status = EffectStatus.ACTIVE;

			if (effect.particles != null) {
				effect.particles.Play ();
			}
		} else {
			effect.status = EffectStatus.HIDDEN;
			GameObject g = activeEffectProps [effect.type];
			activeEffectProps.Remove (effect.type);
			Destroy (g);
			if (effect.particles != null) {
				effect.particles.Stop ();
			}
		}
	}

	public void setPowerupNotifyFlash(Powerup.Type type){
		Effect effect = effects.First(x => x.type == type);

		effect.status = EffectStatus.FLASHING;
	}

    public void playItemParticleSystem(Item.Type type) {
        switch (type) {
            case Item.Type.STICKY_THROWABLE:
				// no particle system
                break;
		case Item.Type.SUPER_BOOST:
			superBoostEffects.Play ();
                break;
        }

    }

	private Effect GetEfect(Powerup.Type type){

	}

	IEnumerator BoostTrail() {
		
		while (trails.time > 0) {
			yield return new WaitForSeconds(0.1f);
			trails.time = trails.time - 0.1f;
		}

		trails.time = 0;
	}
        
}
