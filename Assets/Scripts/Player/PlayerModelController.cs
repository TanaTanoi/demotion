using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour {

    private Animator[] animators;

	void Start () {
        animators = gameObject.GetComponentsInChildren<Animator>();
	}
	
    public void Push() {
        foreach(Animator a in animators) {
            a.SetTrigger("Push");
        }
    }

    public void Throw() {
         foreach(Animator a in animators) {
            a.SetTrigger("Throw");
        }
    }
}
