using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TooltipHitCompliment : TooltipEventInterface {
    public TooltipScript tooltip;
    private int hitCount;

    string[] compliments = new string[] {
        "Nice one!",
        "Good shot!",
        "Keep it up!",
        "Solid aim!",
        "You're doing great!",
        "Do it again!"
    };

    public override void Trigger() {
        int com = (int)Random.Range(0, compliments.Length);
        tooltip.SetText(compliments[com]);
    }
}
