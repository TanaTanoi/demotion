using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCreator : MonoBehaviour {
	public GameObject Prefab;
	private Vector3 Pos;
	private InputType Input;
	private int Customization;
	private GameObject CurrentPrefab;
 



	void Start () {
        InstantiatePlayer();
	}
	void FixedUpdate () {

	}

	public GameObject InstantiatePlayer(/*Transform Pos, InputType Input, int Customization*/){
		//this.Pos = Pos;
		//this.Input = Input;
		//this.Customization = Customization;
		CurrentPrefab = Instantiate(Prefab);	
		return CurrentPrefab;



	}
}
