using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject DefaultPanel;

    private GameObject selectedPanel;
    private bool isPaused;

	// Use this for initialization
	void Start () {
        selectedPanel = DefaultPanel;
        SwitchMenu(selectedPanel);
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Pause_KB") != 0)
        {
            TogglePause();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        isPaused = !focus;
    }

    private void OnApplicationPause(bool pause)
    {
        isPaused = pause;
    }

    /**
     * Re
     */
    public void TogglePause()
    {
        isPaused = !isPaused;
        SwitchMenu(this.transform.FindChild("Pause_Panel").gameObject);
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

    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
