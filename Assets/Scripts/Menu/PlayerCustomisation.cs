using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCustomisation : MonoBehaviour {

//	public List<Material> skins.skins.outfits;
//	public List<GameObject> skins.skins.hats;
//	public List<GameObject> skins.lances;
//
	public GameObject setupObject;
	private PlayerSkins skins;
	private GameSetup setup;

	List<GameObject> hatObjects = new List<GameObject> ();
	List<GameObject> lanceObjects = new List<GameObject>();

	int outfitIndex = 0;
	int hatIndex = 0;
	int lanceIndex = 0;

	Material newMat;
	GameObject currentHat;
	GameObject currentLance;

	GameObject characterObject;

	public int playerNo;

	// Use this for initialization
	void Start () {
		
		skins = setupObject.GetComponent<PlayerSkins> ();
		setup = setupObject.GetComponent<GameSetup> ();
		CustomisationSetup();
	}

	void CustomisationSetup(){
		// get positions for the models

			
		Vector3 playerPosition = this.gameObject.transform.position;
		Transform[] children = this.gameObject.GetComponentsInChildren<Transform>();

		Transform character = null;
		Transform head = null;
		//Vector3 lancePosition = new Vector3(0,0,0);
		Transform lancePosition = null;

		foreach (Transform child in children) {
			if (child.CompareTag("Character")) {
				character = child;
			}
			if(child.CompareTag("LanceLocation")){
				//lancePosition = child.position;
				lancePosition = child;
			}
			if(child.CompareTag("Head")){
				head = child;
			}
		}

		characterObject = character.gameObject;
			
		// instantiate all skins.hats and hide them
		foreach (GameObject hat in skins.hats) {
			currentHat = Instantiate (hat) as GameObject;
			currentHat.transform.position = playerPosition;
			currentHat.transform.parent = head;
			currentHat.transform.localScale = Vector3.one;
			currentHat.SetActive (false);
			hatObjects.Add (currentHat);
		}

		Debug.Log (hatObjects.Count);
		// instantiate all skins.lances and hide them
		foreach (GameObject lance in skins.lances) {
			currentLance = Instantiate (lance) as GameObject;
			currentLance.transform.position = lancePosition.position;
			currentLance.transform.parent = lancePosition;
			currentLance.transform.Rotate (90, 0, 0);
			currentLance.SetActive (false);
			currentLance.transform.localScale = Vector3.one;
			lanceObjects.Add (currentLance);
		}
	
		// show first hat and lance
		currentHat = hatObjects[hatIndex];
		currentLance = lanceObjects[hatIndex];
		hatObjects[hatIndex].SetActive(true);
		lanceObjects[lanceIndex].SetActive(true);



		Debug.Log (character);
		// setting the first out
		newMat = skins.outfits[outfitIndex] as Material;
		characterObject.GetComponent<Renderer> ().material = newMat;

	}


	/*
	 *  Applies customisation to player using index
	 */
	public void EditPlayer(int outfit, int hat, int lance){

		Debug.Log ("editing player " + outfit + " " + hat + " " + lance);

		// setting the outfit
		newMat = skins.outfits[outfit] as Material;

		characterObject.GetComponent<Renderer> ().material = newMat;

		if (hatObjects [hat] != currentHat) {
			currentHat.SetActive (false);
			currentHat = hatObjects [hat];
			currentHat.SetActive (true);
		}

		if (lanceObjects [lance] != currentLance) {
			currentLance.SetActive (false);
			currentLance = lanceObjects [lance];
			currentLance.SetActive (true);
		}
			
	}

	/*
	 *  Is called from UI
	 */
	public void RightButtonOutfit()
	{

		outfitIndex = (outfitIndex + 1)%skins.outfits.Count;

		EditPlayer (outfitIndex, hatIndex, lanceIndex);
	}

	/*
	 *  Is called from UI
	 */
	public void LeftButtonOutfit(){

		outfitIndex = (outfitIndex == 0) ? skins.outfits.Count - 1 : outfitIndex - 1;

		EditPlayer (outfitIndex, hatIndex, lanceIndex);
	}

	/*
	 *  Is called from UI
	 */
	public void RightButtonHat(){
		
		hatIndex = (hatIndex + 1)%hatObjects.Count;


		EditPlayer (outfitIndex, hatIndex, lanceIndex);
	}

	/*
	 *  Is called from UI
	 */
	public void LeftButtonHat(){

		hatIndex = (hatIndex == 0) ? hatObjects.Count - 1 : hatIndex - 1;

		EditPlayer (outfitIndex, hatIndex, lanceIndex);
	}

	/*
	 *  Is called from UI
	 */
	public void RightButtonLance(){
		lanceIndex = (lanceIndex + 1)%lanceObjects.Count;

		EditPlayer (outfitIndex, hatIndex, lanceIndex);
	}

	/*
	 *  Is called from UI
	 */
	public void LeftButtonLance(){

		lanceIndex = (lanceIndex == 0) ? lanceObjects.Count - 1 : lanceIndex - 1;

		EditPlayer (outfitIndex, hatIndex, lanceIndex);
	}

	public void ApplySkins(){
		setup.PopulateSkin (playerNo, new SkinIndexs (outfitIndex, hatIndex, lanceIndex));

		Debug.Log (playerNo + "Ready");
	}

}
