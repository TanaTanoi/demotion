using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinEmoteScript : MonoBehaviour {

	//public float spinSpeed = 110.0f;
	public float scaleSpeed = 0.8f;
	public Transform target;
	void start(){
		target = Camera.main.transform;
	}
	void Update () {
		target = Camera.main.transform;
		//transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
		transform.localScale += new Vector3(-scaleSpeed, -scaleSpeed, -scaleSpeed);
		transform.LookAt(target);
		if (transform.localScale.x <= 0) {
			Destroy (gameObject);
		}
	}
}
