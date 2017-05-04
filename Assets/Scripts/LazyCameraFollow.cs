using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyCameraFollow : MonoBehaviour {

    public Transform playerSubject;
    public Transform menuSubject;


    public bool paused;

    private Vector3 playerOffset = new Vector3(0.7f, 6.8f, -5.1f);
    private Quaternion playerRot;
    private Vector3 menuOffset = new Vector3(0, 10, -1);

    private Transform currentSubject;
    private Vector3 currentOffset;

	// Use this for initialization
	void Start () {
        playerRot = transform.rotation;
        currentOffset = playerOffset;
        currentSubject = playerSubject;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 delta = (currentSubject.position + currentOffset) - transform.position;
        transform.position += delta * 0.1f;
        if(paused) {
            transform.LookAt(currentSubject);
        }
	}

    public void Pause() {
        paused = true;
        currentSubject = menuSubject;
        currentOffset = menuOffset;
    }

    public void Unpause() {
        paused = false;
        currentSubject = playerSubject;
        currentOffset = playerOffset;
        transform.rotation = playerRot;
    }
}
