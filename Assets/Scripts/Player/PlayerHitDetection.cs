using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {

    public float hitThreshold;
    public float invulnerabilityDuration = 5f;

    private float vulnerablility = 0.0f;

    private GameController gameControl;

    private void Start()
    {
        gameControl = FindObjectOfType<GameController>();
    }


    void OnCollisionEnter(Collision collision)
    {
        // Do nothing if not hit with a lance or is still invulnerable
        if (!collision.collider.CompareTag("Lance")) return;
        if (Time.time < vulnerablility) return;

        
        // If we have been hit with enough force, play a sound. 
        if(collision.relativeVelocity.magnitude > hitThreshold)
        {
            //Play hit sound here
            Debug.Log("I got hit!");
            //gameControl.RemoveLife(GetComponentInParent<PlayerInput>().playerNumber);
            GetComponentInChildren<Rigidbody>().AddRelativeForce(Vector3.up);
            vulnerablility = Time.time + invulnerabilityDuration;
        }
    }
}
