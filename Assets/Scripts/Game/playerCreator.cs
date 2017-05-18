using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour {
	public GameObject Prefab;
	//private GameObject CurrentPrefab;
    private PlayerMovement playerMovement;
    //public InputType chooseInput = InputType.Keyboard;
 

	void Start () {
        //InstantiatePlayer(gameObject.transform,chooseInput);
	}

	public GameObject CreatePlayer(Transform spawnPosition, InputType input/*, int Customization*/){

        GameObject CurrentPrefab = Instantiate(Prefab, spawnPosition);

        CurrentPrefab.GetComponentInChildren<PlayerMovement>().setInput(input);

		return CurrentPrefab;
	}
}
