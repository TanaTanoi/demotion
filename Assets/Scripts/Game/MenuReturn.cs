using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuReturn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReturnToMenu(){
		Transform Arena = GameObject.Find ("Arena").transform;
		foreach (Transform child in Arena) {
			Destroy (child.gameObject);
		}
		Debug.Log ("Menu Returned");
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
}
