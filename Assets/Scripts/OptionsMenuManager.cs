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
        SwitchMenu(VisualMenu);
    }

    /**
     * Switches to the audio menu
     */
    public void SwitchToAudioMenu()
    {
        SwitchMenu(AudioMenu);
    }

    /**
     * Switches to the controls menu
     */
    public void SwitchToControlsMenu()
    {
        SwitchMenu(ControlsMenu);
    }

    /**
     * Switches to the data menu
     */
    public void SwitchToDataMenu()
    {
        SwitchMenu(DataMenu);
    }

    /**
     * If there is a change in menu,
     * deactivates the old menu
     * and activates the new one.
     */
    private void SwitchMenu(GameObject menu)
    {
        if(selectedMenu != menu)
        {
            selectedMenu.SetActive(false);
            selectedMenu = menu;
            selectedMenu.SetActive(true);
        }
    }
}
