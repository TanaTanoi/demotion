using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyFollowText : MonoBehaviour {

    public Transform subject;

    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - subject.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = subject.position + offset;
	}
}
