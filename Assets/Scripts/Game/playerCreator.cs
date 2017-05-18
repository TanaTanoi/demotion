using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerCreator : MonoBehaviour {
	public GameObject Prefab;
	//private GameObject CurrentPrefab;

    private PlayerMovement playerMovement;
    public int PlayerCust = 1;


	public GameObject CreatePlayer(Transform Pos ,InputType Input, int Customization){

        string matPath = "Assets/Materials&Textures/Player/player"+Customization+".mat";
        Material newMat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));

        GameObject CurrentPrefab = Instantiate(Prefab, spawnPosition);

        CurrentPrefab.GetComponentInChildren<PlayerMovement>().setInput(Input);
        CurrentPrefab.GetComponentInChildren<SetMaterial>().setMat(newMat);


        return CurrentPrefab;
	}
}
