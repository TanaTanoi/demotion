using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject DefaultPanel;

    private GameObject selectedPanel;

	// Use this for initialization
	void Start () {
        selectedPanel = DefaultPanel;
        SwitchMenu(selectedPanel);
    }

    /**
     * If we become enabled reset to defaults
     */
    private void OnEnable()
    {
        Start();
    }

    /**
     * Enables the given menu panel and disables the current one if they are different
     */
    public void SwitchMenu(GameObject menu)
    {
        if(menu != selectedPanel)
        {
            selectedPanel.SetActive(false);
            selectedPanel = menu;
            selectedPanel.SetActive(true);
        }
    }
}
