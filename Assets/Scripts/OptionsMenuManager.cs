using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsMenuManager : MonoBehaviour {

    public GameObject VisualMenu;
    public GameObject AudioMenu;
    public GameObject ControlsMenu;
    public GameObject DataMenu;

    private GameObject selectedMenu = null;


	void Start () {
        if(VisualMenu == null)
        {
            Debug.LogError("Visual menu must be assigned");
        } else
        {
            // Default menu is the visuals menu
            selectedMenu = VisualMenu;
        }

	}

    /**
     * Switches to the visual menu
     */
    public void SwitchToVisualMenu()
    {
        //MenuController.SwitchMenu(VisualMenu, selectedMenu);
    }

    /**
     * Switches to the audio menu
     */
    public void SwitchToAudioMenu()
    {
        //MenuController.SwitchMenu(AudioMenu, selectedMenu);
    }

    /**
     * Switches to the controls menu
     */
    public void SwitchToControlsMenu()
    {
        //.SwitchMenu(ControlsMenu, selectedMenu);
    }

    /**
     * Switches to the data menu
     */
    public void SwitchToDataMenu()
    {
        //MenuController.SwitchMenu(DataMenu, selectedMenu);
    }

}
