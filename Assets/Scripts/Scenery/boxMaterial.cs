using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boxMaterial : MonoBehaviour
{
    private ParticleSystem psystem;
	private int paperCount=3;
    void Start()
    {
		psystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.tag == "Player"&&paperCount>0) { 
            psystem.Play();
			paperCount--;
         }
    }
}
    