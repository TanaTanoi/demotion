using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerCreator : MonoBehaviour {

    public GameObject Prefab;
    //private GameObject CurrentPrefab;
    private PlayerMovement PlayerMovement;
    public InputType chooseInput = InputType.Keyboard;
    private PlayerInput pInput;
    public int PlayerCust = 1;


    void Start()
    {
        // This should only be done for debugging
        switch(chooseInput)
        {
            case InputType.Keyboard:
                pInput = new InputKeyboard();
                break;
            case InputType.Mouse:
                pInput = new InputMouse();
                break;
            case InputType.Controller:
                pInput = new InputController();
                break;
        }
    }


    public GameObject CreatePlayer(Transform Pos, PlayerInput input, int Customization)
    {

        string matPath = "Assets/Materials&Textures/Player/player" + Customization + ".mat";
        Material newMat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));

        GameObject CurrentPrefab = Instantiate(Prefab, Pos);

        CurrentPrefab.GetComponentInChildren<PlayerMovement>().SetInput(input);
        CurrentPrefab.GetComponentInChildren<SetMaterial>().setMat(newMat);


        return CurrentPrefab;
    }
}
