using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text timerText;
    public float timeLeft = 99;

    public int numberPlayers;
    private int playerCount = 0;

    public GameObject playerPrefab;

    // Empty game object cotaining all of the possible spawn points
    public Transform spawnPoints;

    // List of HUD elements where the players' lives are shown. // Change this to not drag and drop?
    public GameObject[] playersHUD = new GameObject[4];

    //private Dictionary<int, GameObject> playersDict; //reimple

    private void Awake()
    {
        // Don't kill me :(
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        //playersDict = new Dictionary<int, GameObject>(); // reimplement after playtesting day

        RestartGame();
    }

    // Update is called once per frame
    void Update() {
        UpdateTime();
        
    }

    /**
     * Spawns the given player at a random spawn location
     */
    void SpawnPlayer(PlayerNumber player)
    {
        int spawnIndex = (int)Random.Range(0, spawnPoints.childCount-1);
        Transform location = spawnPoints.GetChild(spawnIndex).transform;
        
    }

    /**
     * Reduces the lives remaining of the given player
     */
    void LoseLife(PlayerNumber player)
    {

    }

    /**
     * Updates the time remaining of the match
     */
    void UpdateTime()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + (int)timeLeft;

        if (timeLeft <= 0)
        {
            PauseGame();
        }
    }
     
    /**
     * Pauses the game and opens the pause menu
     */
    void PauseGame()
    {
        Time.timeScale = 0;
        //FindObjectOfType<MenuController>().TogglePause();
    }

    /**
     * Restarts the game, resetting the time, players, lives, etc
     */
    public void RestartGame()
    {
        // Pause the game
        PauseGame();
        // Enable player HUDS for each player in the game
        for (int i = 0; i < numberPlayers; i++)
        {
            playersHUD[i].SetActive(true);
        }
    }

    
}
