using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TooltipHitCounter : TooltipEventInterface{
    public TooltipScript tooltip;
    private int hitCount;

    void Start() {
    }

    public override void Trigger() {
        hitCount++;
        SetText();
    }

    public int GetHits() {
        return hitCount;
    }

    private void SetText() {
        string hit = " hit";
        hit += hitCount > 1 ? "s" : "";
        tooltip.SetText("Nice! " + hitCount + hit);
    }

    public void SetHits(int hit) {
        hitCount = hit;
        SetText();
    }
}
