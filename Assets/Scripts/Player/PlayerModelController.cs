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

	public void First(){
		foreach(Animator a in animators) {
			a.SetTrigger("Win");
		}

	}

	public void Second(){
		foreach(Animator a in animators) {
			a.SetTrigger("Second");
		}

	}

	public void Third(){
		foreach(Animator a in animators) {
			a.SetTrigger("Third");
		}

	}
}
