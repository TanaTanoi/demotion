using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerCreator : MonoBehaviour {

    public GameObject Prefab;
    //private GameObject CurrentPrefab;
    private PlayerMovement PlayerMovement;
    public InputType chooseInput = InputType.Keyboard;
    public int PlayerCust = 1;
    

    void Start()
    {
        
    }


    public GameObject CreatePlayer(Transform Pos, InputType input, int Customization)
    {

        string matPath = "Assets/Materials&Textures/Player/player" + Customization + ".mat";
        Material newMat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));

        GameObject CurrentPrefab = Instantiate(Prefab, Pos);

        CurrentPrefab.GetComponentInChildren<PlayerMovement>().SetInput(input);
        CurrentPrefab.GetComponentInChildren<SetMaterial>().setMat(newMat);


        return CurrentPrefab;
    }

}
