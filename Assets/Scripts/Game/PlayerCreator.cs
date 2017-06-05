using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour {

    public GameObject prefab;

    /**
     * Creates a player with the given settings at the given position
     */
    public GameObject CreatePlayer(Vector3 pos, PlayerSettings settings)
    {

        //This will change to use the player customisation
        string matPath = "Assets/Materials&Textures/Player/player" + settings.playerID + ".mat";

        //Material newMat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));


        // TODO reimplement this mat thing but getting around the unity editor issue
        //GameObject CurrentPrefab = (GameObject)Instantiate(Resources.Load("Player"), new Vector3(-5,1,-3), new Quaternion(0,0,0,1));
        //GameObject CurrentPrefab2 = (GameObject)Instantiate(Resources.Load("Lance"), new Vector3(-4,1,-3), new Quaternion(0,0,0,1));


        // Create the player
        GameObject player = Instantiate(prefab, pos, Quaternion.identity);

        // Apply their settings
        PlayerSettings pSettings = player.GetComponent<PlayerSettings>();
        pSettings.input = settings.input;
        pSettings.controllerID = settings.controllerID;
        pSettings.playerID = settings.playerID;
        pSettings.teamID = settings.teamID;
        // Tell the movement script to add the appropriate input type
        player.GetComponentInChildren<PlayerMovement>().SetInput();

        //CurrentPrefab.GetComponentInChildren<SetMaterial>().setMat(newMat);

        return player;
    }

}
