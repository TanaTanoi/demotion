using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {

    public float hitThreshold;
    public ParticleSystem particleSystem;
    public int particles;
    private GameController gameControl;

    private void Start()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        // If we have been hit with enough force, play a sound. 
        if(collision.relativeVelocity.magnitude > hitThreshold)
        {
            //Play hit sound here
            particleSystem.Emit(particles);
        }
    }
}
