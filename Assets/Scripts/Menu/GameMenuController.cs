using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuController : MonoBehaviour {

    public TooltipHitCounter[] counters;
    public PlayerItemController playerItem;
    public PlayerMovement playerMovement;
    public UnityEngine.UI.Button loadButton;

    public LazyCameraFollow cam;
    private bool paused;
	// Use this for initialization
	void Start () {
        new WaitForSeconds(0.1f);
        Pause();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
            if(paused) {
                Unpause();
            } else {
                Pause();
            }
        }

	}

    public void SaveScores() {
        string[] lines = new string[counters.Length];
        for(int i = 0; i < lines.Length; i++) {
            lines[i] = counters[i].GetHits() + " ";
        }
        System.IO.File.WriteAllLines("demotion_scores.txt", lines);
    }

    public void LoadScores(){
        if(System.IO.File.Exists("demotion_scores.txt")) {
            string[] lines = System.IO.File.ReadAllLines("demotion_scores.txt");
            for(int i = 0; i < counters.Length; i++) {
                counters[i].SetHits(int.Parse(lines[i]));
            }
        } else {
            Debug.Log("Cannot load from nothing");
        }
    }

    public void Pause() {
        paused = true;
        loadButton.interactable = System.IO.File.Exists("demotion_scores.txt");
        playerItem.enabled = false;
        playerMovement.enabled = false;
        Time.timeScale = 0f;
        cam.Pause();
    }

    public void Unpause() {
        paused = false;
        playerItem.enabled = true;
        playerMovement.enabled = true;
        Time.timeScale = 1f;
        cam.Unpause();
    }
}
