using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteWrapper : MonoBehaviour {
	public GameObject[] prefabs;
	GameObject obj;
	// Use this for initialization
	void Start () {
		obj = (GameObject) Instantiate(prefabs[Random.Range(0,6)],transform.position, transform.rotation);
	}
}
