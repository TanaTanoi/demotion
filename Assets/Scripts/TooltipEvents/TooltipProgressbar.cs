using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipProgressbar : MonoBehaviour {

    public RectTransform panel;
    private Vector3 originalScale;
    private Vector3 originalPos;

    private float max = 4.5f;
	void Start () {
        originalPos = panel.localPosition;
        originalScale = panel.localScale;
	}

    public void SetProgress(float percent) {

        percent = percent * 4.5f;

        panel.localScale =  (Vector3.right * 0.1f * percent) + (originalScale.x * Vector3.up);
        panel.localPosition = originalPos +  (Vector3.right * percent * 50);
    }
}
