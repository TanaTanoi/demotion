using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCycleScript : MonoBehaviour {
	Image myImage;
	public Sprite[] sprites;
	private int counter = 0;
	private float timeCount = 0.0f;
	public float switchTime = 0.4f;
	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update () {
		timeCount += Time.deltaTime;
		if (timeCount > switchTime){
			counter += 1;
			if (counter > 4){
				counter = 0;
			}
			GetComponent<Image>().overrideSprite = sprites[counter];
			timeCount = 0;
		}
	}
}
