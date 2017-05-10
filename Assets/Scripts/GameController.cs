using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text timerText;
    public float timeLeft = 99;

    public GameObject[] playersHUD;

    public static int players = 0;

	// Use this for initialization
	void Start () {
        //players = FindObjectsOfType<PlayerInput>();
        //Debug.Log("There are " + players.Length + " players in this game.");
        timerText.text = "Time: " + (int)timeLeft;

        for(int i = 0; i < players; i++)
        {
            playersHUD[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + (int)timeLeft;
        if(timeLeft <= 0)
        {
            Time.timeScale = 0;
            FindObjectOfType<MenuController>().TogglePause();
        }
    }

     


    
}
