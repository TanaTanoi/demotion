using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerCreator : MonoBehaviour {

    public GameObject prefab;
    //private GameObject CurrentPrefab;
    private PlayerMovement PlayerMovement;
    public InputType chooseInput = InputType.Keyboard;
    public int PlayerCust = 1;
    public bool debug = false;
    
    void Start()
    {
		
        if(debug)
            CreatePlayer(transform.position, chooseInput, 0);
    }

    public GameObject CreatePlayer(Vector3 Pos, InputType input, int Customization)
    {
        string matPath = "Assets/Materials&Textures/Player/player" + Customization + ".mat";
        Material newMat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));

        GameObject CurrentPrefab = Instantiate(prefab);
		//GameObject CurrentPrefab = (GameObject)Instantiate(Resources.Load("Player"), new Vector3(-5,1,-3), new Quaternion(0,0,0,1));
		//GameObject CurrentPrefab2 = (GameObject)Instantiate(Resources.Load("Lance"), new Vector3(-4,1,-3), new Quaternion(0,0,0,1));
        CurrentPrefab.transform.position = Pos;

        CurrentPrefab.GetComponentInChildren<PlayerMovement>().SetInput(input);
		CurrentPrefab.GetComponentInChildren<PlayerMovement>().SetPlayerNum(Customization);

        //CurrentPrefab.GetComponentInChildren<SetMaterial>().setMat(newMat);


        return CurrentPrefab;
    }

}
