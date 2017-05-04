using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that tries to keep the dummy upright, like a punching bag
public class TargetDummyScript : MonoBehaviour {

    public ParticleSystem[] particleSystems;
    private TooltipEventInterface tip;
	// Use this for initialization
	void Start () {
        tip = GetComponent<TooltipEventInterface>();
	}
	
    void Update() {
        Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up);
        GetComponent<Rigidbody>().AddTorque(new Vector3(q.x, q.y, q.z) * 100f);
    }

    void OnCollisionEnter(Collision col) {
        if(col.relativeVelocity.magnitude > 9) {
            if(tip != null) {
                tip.Trigger();
            }
            foreach(ParticleSystem ps in particleSystems) {
                ps.Play();
            }
        }
    }
}
