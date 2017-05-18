using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class playerCreator : MonoBehaviour {
	public GameObject Prefab;
	//private GameObject CurrentPrefab;
    private PlayerMovement PlayerMovement;
    public InputType ChooseInput = InputType.Keyboard;
    public int PlayerCust = 1;

 



	void Start () {
        InstantiatePlayer(gameObject.transform,ChooseInput,PlayerCust);
	}
	void FixedUpdate () {

	}

	public GameObject InstantiatePlayer(Transform Pos ,InputType Input, int Customization){

        string matPath = "Assets/Materials&Textures/Player/player"+Customization+".mat";
        Material newMat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));

        GameObject CurrentPrefab = Instantiate(Prefab, Pos);

        CurrentPrefab.GetComponentInChildren<PlayerMovement>().setInput(Input);
        CurrentPrefab.GetComponentInChildren<SetMaterial>().setMat(newMat);


        return CurrentPrefab;



	}
}
