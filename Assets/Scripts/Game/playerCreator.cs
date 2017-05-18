using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCreator : MonoBehaviour {
	public GameObject Prefab;
	//private GameObject CurrentPrefab;
    private PlayerMovement playerMovement;
 



	void Start () {
        InstantiatePlayer(gameObject.transform,InputType.Keyboard);
	}
	void FixedUpdate () {

	}

	public GameObject InstantiatePlayer(Transform Pos ,InputType Input/*, int Customization*/){

        GameObject CurrentPrefab = Instantiate(Prefab, Pos);

        CurrentPrefab.GetComponentInChildren<PlayerMovement>().setInput(Input);

		return CurrentPrefab;



	}
}
