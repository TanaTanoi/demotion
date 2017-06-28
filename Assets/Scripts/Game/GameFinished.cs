using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinished : MonoBehaviour {

	public GameObject playerFirst;
	public GameObject playerSecond;
	public GameObject playerThird;

	public List<GameObject> topThree;
	public GameObject locationOne;
	public GameObject locationTwo;
	public GameObject locationThree;

	private PlayerModelController playerAnimator;

	// Use this for initialization
	void Start () {
		PopulateTopThree ();
	}

	public void PopulateTopThree(){
		topThree = new List<GameObject> { playerFirst, playerSecond, playerThird } ();

		// set top 3 players

	}

	public void ShowPodium(){

		// move the camera
		GameObject currentCamera = GameObject.Find("Main Camera");
		Camera ccamera = currentCamera.GetComponent<Camera> ();

		GameObject finishedCamera = GameObject.Find("FinishedCamera");
		Camera fcamera = finishedCamera.GetComponent<Camera> ();

		ccamera.enabled = false;
		fcamera = true;

	}

	public void SetPlayers(){
		
		foreach(GameObject player in topThree) {
			ApplyPlayerPosition (player);
		}

	}

	public void ApplyPlayerPosition(GameObject player){
		playerAnimator = player.GetComponentInChildren<PlayerModelController>();

		if(player == playerFirst){
			// move the player
			playerFirst.transform.position = locationOne.transform.position;

			// destroy the chair and lance
			foreach (Transform child in playerFirst.transform){
				if (child.tag == "Chair") {
					Destroy (child);
				}
				if (child.tag == "Lance") {
					Destroy (child);
				}
			}

			// apply the animation
			playerAnimator.First();

		} else if(player.Equals(playerSecond)){

			// move the player
			playerFirst.transform.position = locationTwo.transform.position;

			// destroy the chair and lance
			foreach (Transform child in playerFirst.transform){
				if (child.tag == "Chair") {
					Destroy (child);
				}
				if (child.tag == "Lance") {
					Destroy (child);
				}
			}

			// apply the animation
			playerAnimator.Second();
		} else if(player.Equals(playerThird)){
			
			// move the player
			playerFirst.transform.position = locationThree.transform.position;

			// destroy the chair and lance
			foreach (Transform child in playerFirst.transform){
				if (child.tag == "Chair") {
					Destroy (child);
				}
				if (child.tag == "Lance") {
					Destroy (child);
				}
			}

			// apply the animation
			playerAnimator.Third();
		}
	}
}
