using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {

    public float hitThreshold;

    private GameController gameControl;

    private void Start()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision: " +Time.time);
        // If we have been hit with enough force, play a sound. 
        if(collision.relativeVelocity.magnitude > hitThreshold)
        {
            //Play hit sound here

            // If we have been hit by a hostile lance then lose a life or something
            if(collision.gameObject.CompareTag("Lance"))
            {
                Debug.Log("I've been attacked!");
            }
        }
    }
}
