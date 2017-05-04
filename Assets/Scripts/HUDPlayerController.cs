using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPlayerController : MonoBehaviour {
    public PlayerInput.PlayerNumber playerNumber;
    public GameObject[] lives;
    public int respawnTime = 10;
    private int currentLives;

    private void Start()
    {
        currentLives = lives.Length-1;
    }

    public void loseLife()
    {
        lives[currentLives].SetActive(false);
        currentLives--;
        if(currentLives == -1)
        {
            //Player dies and must respawn
            Debug.Log("Player: " + playerNumber + " is out of lives! Respawning in " + respawnTime + " seconds..");
        }
    }
}
