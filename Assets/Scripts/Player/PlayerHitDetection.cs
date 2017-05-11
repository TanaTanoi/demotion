using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {

    public float hitThreshold;
    private GameController gameControl;

    private void Start()
    {
        gameControl = FindObjectOfType<GameController>();
    }


    void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Lance")) return;
        // If we have been hit with enough force, play a sound. 
        if(collision.relativeVelocity.magnitude > hitThreshold)
        {
            //Play hit sound here
            Debug.Log("I got hit!");
            //gameControl.RemoveLife(GetComponentInParent<PlayerInput>().playerNumber);
        }
    }
}
