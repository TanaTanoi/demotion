using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CreditsCrawl : MonoBehaviour {

    public int startPosition;
    public GameObject credits;
    public Slider speedSlider;
    public Text speedText;

    private float speed;


	// Use this for initialization
	void Start () {
        credits.transform.localPosition = new Vector3(0, startPosition, 0);
        speedSlider.value = 100;
        setSpeed();
    }

    // Update is called once per frame
    void Update() {
        if ((credits.transform.localPosition.y > 0 && speed > 0)
            || (credits.transform.localPosition.y < startPosition && speed < 0)) {
            return;
        }
        credits.transform.Translate(Vector3.up * Time.deltaTime * speed);
	}

    public void setSpeed()
    {
        speed = speedSlider.value;
        speedText.text = "Speed: " + speed;
    }

    private void OnEnable()
    {
        Start();
    }
}
