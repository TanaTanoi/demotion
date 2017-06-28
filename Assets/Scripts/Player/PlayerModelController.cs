using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour {

    private Animator[] animators;

	void Start () {
		SetupAnimators ();
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
		SetupAnimators ();
		foreach(Animator a in animators) {
			a.SetTrigger("Win");
		}

	}

	public void Second(){
		SetupAnimators ();
		foreach(Animator a in animators) {
			a.SetTrigger("Second");
		}

	}

	public void Third(){
		SetupAnimators ();
		foreach(Animator a in animators) {
			a.SetTrigger("Third");
		}

	}

	private void SetupAnimators(){
		animators = gameObject.GetComponentsInChildren<Animator>();
	}
}
