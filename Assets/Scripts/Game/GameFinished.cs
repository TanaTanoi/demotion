﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinished : MonoBehaviour{

	public List<int> topThree;
	public GameObject locationOne;
	public GameObject locationTwo;
	public GameObject locationThree;

	private PlayerCreator playerCreator;
	private PlayerSettings[] playerSettings;
	// Use this for initialization
	void Start(){
		playerCreator = GameObject.FindObjectOfType<GameController> ().playerCreator;
	}

	public void FinishGame(List<int> winners, PlayerSettings[] playerSettings){
		this.playerSettings = playerSettings;
		topThree = winners;
		SetPlayers ();
		ShowPodium ();
	}
		
	public void ShowPodium(){

		// move the camera
		GameObject currentCamera = GameObject.Find("Main Camera");
		Camera ccamera = currentCamera.GetComponent<Camera> ();

		GameObject finishedCamera = GameObject.Find("FinishedCamera");

		ccamera.enabled = false;
		finishedCamera.transform.GetChild (0).gameObject.SetActive (true);

	}

	public void SetPlayers(){
		for (int i = 0; i < 3 && i < topThree.Count; i++) {
			ApplyPlayerPosition (topThree [i], i);
		}

	}

	public void ApplyPlayerPosition(int playerID, int tier){
		Debug.Log ("Applying " + playerID + " To pos " + tier);
		GameObject player = FindObjectOfType<GameController>().MakePlayer(Vector3.up * 10, playerID);
		PlayerModelController playerAnimator = player.GetComponentInChildren<PlayerModelController>();
		player.GetComponentInChildren<PlayerMovement> ().SetRotationSpeed (0); // disables input
		Vector3 campos = Camera.main.transform.position;
		player.transform.rotation.SetLookRotation (campos - player.transform.position);
		if(tier == 0){
			// move the player
			player.transform.position = locationOne.transform.position;
			// apply the animation
			playerAnimator.First();
		} else if(tier == 1){
			// move the player
			player.transform.position = locationTwo.transform.position;
			// apply the animation
			playerAnimator.Second();
		} else if(tier == 2){
			// move the player
			player.transform.position = locationThree.transform.position;
			// destroy the chair and lance
			playerAnimator.Third();
		}
		
		Destroy (player.GetComponentInChildren<PlayerHitDetection> ().gameObject); // destroy lance
		
		// Should destroy chair
		foreach (Transform child in player.transform.GetChild(0).transform){
			if (child.tag == "Chair") {
				Destroy (child.gameObject);
			}
		}
	}
}
