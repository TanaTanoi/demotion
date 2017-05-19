using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerCreator : MonoBehaviour {

    public GameObject Prefab;
    //private GameObject CurrentPrefab;
    private PlayerMovement PlayerMovement;
    public InputType ChooseInput = InputType.Keyboard;
    public int PlayerCust = 1;
    private int playerCount;

    void Start()
    {
        // Get the number of possible players between 2 and 4.
        playerCount = (int)Mathf.Clamp((Input.GetJoystickNames().Length), 2, 4);
    }


    public GameObject CreatePlayer(Transform Pos, InputType Input, int Customization)
    {

        string matPath = "Assets/Materials&Textures/Player/player" + Customization + ".mat";
        Material newMat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));

        GameObject CurrentPrefab = Instantiate(Prefab, Pos);

        CurrentPrefab.GetComponentInChildren<PlayerMovement>().setInput(Input);
        CurrentPrefab.GetComponentInChildren<SetMaterial>().setMat(newMat);


        return CurrentPrefab;
    }

    public int GetPlayerCount()
    {
        return playerCount;
    }
}
