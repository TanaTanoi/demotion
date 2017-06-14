using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour {
    public static GameController instance;
	private static CameraController mainCamera;

    /*== PLAYER SETTINGS ==*/
   
	private PlayerSettings[] playerSettings;
	private GameObject[] players;

	private PlayerSkins skins;
    private PlayerCreator playerCreator;
    private Transform spawnPoints;

    /*== GAME STATUS ==*/
    private bool paused = false;
	private bool playing = false;
    private GameSettings settings;
	private RoundManager roundManager;

	/*== MENU SETTINGS ==*/
	public GameObject menu;  // MenuController handles all the UI elements
	private MenuController menuControl;
    public Text timerText;
    // Access the player hud elements here from a child object of something
    // also get the textbox for the timer here

    private float currentRoundDuration;
	public List<GameObject> crackedTiles;
    public int dropInterval = 20;
	private int drop = 0;

	/*== CAMERA SETTINGS ==*/
	private bool zooming = false;

    /*=== INITIALISATION ===*/


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }

		menuControl = menu.GetComponent<MenuController> ();
		playerCreator = GetComponent<PlayerCreator> ();
		roundManager = gameObject.AddComponent<DeathMatchRoundManager> ();
		mainCamera = FindObjectOfType<CameraController> ();
    }

	public void SetGameSettings(GameSettings gameSettings, PlayerSkins skins)
    {
        settings = gameSettings;
		this.skins = skins;
		playerSettings = new PlayerSettings[settings.playerCount];
		players = new GameObject[settings.playerCount];
        Restart();
    }

    public void Restart()
    {
        timerText.text = "Time: " + settings.roundDuration;
        StartGame();
    }

    /**
     * Spawns all the players
     */
    void SpawnAllPlayers()
    {
		spawnPoints = GameObject.Find("SpawnPoints").transform;
        for(int i = 0; i < settings.playerCount; i++)
        {
            //TODO change the spawn position to be random, change texture to be what the player decided on during customisation
			Respawn(i);
        }
    }

    /*=== END INITIALISATION ===*/

    /*=== GAME LOGIC ===*/
    /**
     * Starts a new game with the current settings, should only be called after applying game mode
     */
    void StartGame()
    {
		StartRound (1);

    }

    /**
	 * The start of a new round
	 */
    void StartRound(int roundNumber) {
        //TODO display start round splash: 3..2..1..Joust (or something)
        Debug.Log(string.Format("Starting round {0}", roundNumber));
        currentRoundDuration = settings.roundDuration;  // Reset the round duration
		drop = (int)currentRoundDuration - 20;
        SpawnAllPlayers();
		playing = true;
		paused = false;
	}

    // Update is called once per frame
    void FixedUpdate() {
		if (playing && !paused) {
			UpdateTime ();
			FocusCamera ();
		}
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    /**
     * Updates the time remaining of the match
     */
    void UpdateTime()
	{
        // TODO change this to not be called if round duration is infinite
		currentRoundDuration -= Time.deltaTime;
		timerText.text = "Time: " + (int)currentRoundDuration;
		if (currentRoundDuration <= 0)
		{
            // TODO end round here
			PauseGame();
		}

		if ((int)currentRoundDuration == drop) {
			if (crackedTiles.Count > 0) {
				DropCrackedCenter ();
			}
			drop -= dropInterval;
		}
	}

	private void FocusCamera(){
		if (!zooming) {
			Vector3 mid = Vector3.zero;
            /* New dict

            int count = IdPlayerDict.CountInner(DictionarySet.first);
            for (int i = 0; i < count; i++)
            {
                mid += IdPlayerDict.GetValue(i).transform.position;
            }
            float biggestDist = 0;
            mid = mid / count;
            for(int i = 0; i < count; i++)
            {
                GameObject p = IdPlayerDict.GetValue(i);
                float distance = Vector3.Distance(p.transform.position, mid);
                biggestDist = Mathf.Max(distance, biggestDist);
            } */
            /* Old dict */
            foreach (GameObject x in players) {
				if(x != null)
					mid += x.transform.position;
			}
			float biggestDist = 0;
			mid = mid / players.Length;

			foreach (GameObject x in players) {
                if (x == null) continue;
				float d = Vector3.Distance (x.transform.position, mid);
				biggestDist = Mathf.Max (d, biggestDist);					
			} 


			mainCamera.SetFocalPoint (mid);
			// 0.3 is the max influence we can get from the base zoom

			mainCamera.desiredBaseZoom = Mathf.Max(0.3f - (biggestDist / 50), 0); // TODO change 50f to some fixed map radius
		}
	}

	/**
     * creates a list of cracked tiles in the arena
     */
	public void CrackedCenterSetup(){
		crackedTiles = GameObject.FindGameObjectsWithTag ("crackedCenter").ToList ();
	}

	/**
     * Drops one cracked tile
     */
	void DropCrackedCenter()
	{
		int randomIndex = Random.Range (0, crackedTiles.Count);
		GameObject crack = crackedTiles [randomIndex];
		crackedTiles.RemoveAt (randomIndex);

		BoxCollider bc = crack.GetComponent<BoxCollider> ();
		bc.isTrigger = true;

		Rigidbody rb = crack.GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		rb.useGravity = true;

	}

    /**
     * Returns true if the players are on opposing teams
     */
    private bool OpposingTeam(int player1, int player2)
    {
		return (settings.players[player1].teamID != settings.players[player2].teamID);
    }

    /**
     * Called from a player when they hit another player.
     * Will lose lives or increase score depending on the game mode.
     * Returns true if the players are on different teams and is therefore a valid hit
     */
    public bool OnHit(int hitter, int hitee)
    {
        if (!OpposingTeam(hitter, hitee)) return false;

		GameObject loser = players [hitee];
		GameObject winner = players [hitter];
		
		Vector3 mid = (winner.transform.position - loser.transform.position) * 0.5f + loser.transform.position;

		StartCoroutine (FocusOnPoint (mid));
		roundManager.OnHit(hitter, hitee);
        return true;
    }

    /**
     * Kills the given player
     */
    public void Kill(GameObject player)
    {
        Destroy(player);
		Respawn(player.GetComponentInParent<PlayerMovement>().settings.playerID);
        //Something about losing points here
    }


	private IEnumerator FocusOnPoint(Vector3 point)
	{
		zooming = true;
		mainCamera.ZoomIn (point);
		for(int i = 0; i < 10; i++) // Should remove these magic numbers
		{
			Time.timeScale -= 0.05f;
		}
		yield return new WaitForSeconds(1f);
		mainCamera.ReturnZoom ();
		for(int i = 0; i < 10; i++)
		{
			Time.timeScale += 0.05f;
		}
		zooming = false;
	}


	private void EndRound() {
		playing = false;
	}
    /*=== END GAME LOGIC ===*/

    /*=== PAUSE LOGIC ===*/
    /**
     * Pause/unpause the game
     */
    public void TogglePause()
    {
        if (!paused)
            PauseGame();
        else
            ResumeGame();
    }

    /**
     * Pauses the game, swaps to the pause menu
     */
    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
        menuControl.Pause();
    }

    /**
     * Unpause the game, swaps back to the in game HUD
     */
    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        menuControl.Resume();
    }
    /*=== END PAUSE LOGIC ===*/

    // Remove this, only game controller should access the round manager directly
	public RoundManager GetRoundManager(){
		return this.roundManager;
	}

	/**
	 * Manages Respawning of players
	 **/
	public void Respawn (int playerNum){
        if(players[playerNum] != null)
        {
            Destroy(players[playerNum]);
            players[playerNum] = null;
        }
		List<Transform> goodSpawns = new List<Transform> ();
		for (int j = 0; j < spawnPoints.transform.childCount; j++) {
			
			if (IsGoodSpawn (j)) {
				goodSpawns.Add (spawnPoints.transform.GetChild (j));
			}
		}
		int i = Random.Range (0, goodSpawns.Count);
        // Wait for respawn time here
		players[playerNum] = playerCreator.CreatePlayer(goodSpawns[i].position, settings.players[playerNum]);

	}

	private bool IsGoodSpawn(int spawnNumber){

		Transform spawn = spawnPoints.GetChild (spawnNumber);
		foreach(GameObject x in players){
			if (x != null) {
				float distance = Vector3.Distance (x.transform.position, spawn.transform.position);
				if (distance < 10) { // Remove magic numbers please
					return false;
				}
			}

		}
		return true;
	}

	public PlayerSkins GetSkins(){
		return skins;
	}
}
