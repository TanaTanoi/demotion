using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObjects : MonoBehaviour {

    //If anything gets here it should be destroyed
    void OnTriggerEnter(Collider other){
        GameObject obj = other.gameObject;
        if (other.transform.root.gameObject.CompareTag("Ragdoll"))
            obj = other.transform.root.gameObject;
        Destroy (obj);
	}
}
