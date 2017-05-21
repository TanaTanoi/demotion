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
    public bool debug = true;
    
    void Start()
    {
        if(debug)
            CreatePlayer(transform.position, chooseInput, 0);
    }

    public GameObject CreatePlayer(Vector3 Pos, InputType input, int Customization)
    {
        string matPath = "Assets/Materials&Textures/Player/player" + Customization + ".mat";
        Material newMat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));

        GameObject CurrentPrefab = Instantiate(Prefab);
        CurrentPrefab.transform.position = Pos;

        CurrentPrefab.GetComponentInChildren<PlayerMovement>().SetInput(input);
        CurrentPrefab.GetComponentInChildren<SetMaterial>().setMat(newMat);


        return CurrentPrefab;
    }

}
