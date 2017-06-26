using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour {

    public GameObject prefab;
	private GameController controller;
	private PlayerSkins skins;

	public void Initialise(){
		controller = GameController.instance;
		skins = controller.GetSkins ();

		Debug.Log (skins.hats.ToString ());
		Debug.Log (skins.hats[1].name);
	}
    /**
     * Creates a player with the given settings at the given position
     */
    public GameObject CreatePlayer(Vector3 pos, PlayerSettings settings)
    {

        // Create the player
        GameObject player = Instantiate(prefab, pos, Quaternion.identity);
		PlayerLimbs limbs = player.GetComponent<PlayerLimbs> ();

		// Setting the player customisation using player limbs
		GameObject hat = skins.hats [settings.indices.hatIndex];
		GameObject playerHat = Instantiate (hat, limbs.head.transform.position, limbs.head.transform.rotation);
		playerHat.transform.parent = limbs.head.transform;

		Mesh lance = skins.lances [settings.indices.lanceIndex].GetComponentInChildren<MeshFilter>().sharedMesh;
		Mesh playerLance = Instantiate (lance);
		limbs.lance.GetComponent<MeshFilter> ().sharedMesh = playerLance;

		player.GetComponent<PlayerLimbs> ().character.GetComponent<Renderer> ().material = skins.outfits [settings.indices.outfitIndex];
	

		PlayerInput input = new InputKeyboard();
		switch(settings.input)
		{
			case InputType.Keyboard:
				input = new InputKeyboard ();
				(input as InputKeyboard).RefreshInputs(settings.keyboardID);
	            break;
		case InputType.Mouse:
				input = new InputMouse ();
	            break;
			case InputType.Controller:
				input = new InputController ();
				(input as InputController).RefreshInputs(settings.controllerID);
	            break;
        }

        // Tell the movement script to add the appropriate input type
        PlayerMovement m = player.GetComponentInChildren<PlayerMovement>();
		m.SetInput (input);
		m.settings = settings;
	

        //CurrentPrefab.GetComponentInChildren<SetMaterial>().setMat(newMat);

        return player;
    }

}
