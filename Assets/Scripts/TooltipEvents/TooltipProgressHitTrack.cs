using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipProgressHitTrack : MonoBehaviour {
    public TooltipHitCounter[] dummies;
    private TooltipProgressbar bar;
    private TooltipScript tooltip;

    public int requiredHitsPerDummy = 2;
	// Use this for initialization
	void Start () {
        bar = GetComponent<TooltipProgressbar>();
        tooltip = GetComponent<TooltipScript>();

        string time = "time";
        time += requiredHitsPerDummy > 1 ? "s" : "";
        tooltip.SetText("Hit each one " + requiredHitsPerDummy + " " + time + " to win!");
	}
	
	// Update is called once per frame
	void Update () {
        float progress = 0;
        foreach(TooltipHitCounter dummy in dummies) {
            int hits = dummy.GetHits();
            hits = Mathf.Min(hits, requiredHitsPerDummy);
            progress += (float)hits / (float)(dummies.Length * requiredHitsPerDummy);
        }
        bar.SetProgress(progress);
        if(progress >= 1) {
            tooltip.SetText("Great Job! You win! Feel free to keep practicing");
        } else {
            string time = "time";
            time += requiredHitsPerDummy > 1 ? "s" : "";
            tooltip.SetText("Hit each one " + requiredHitsPerDummy + " " + time + " to win!");
        }
	}
}
