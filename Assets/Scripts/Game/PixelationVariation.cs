using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelationVariation : MonoBehaviour {
	private Renderer rendGame;
	private float time2Pxl;
	private float pxlTime;
	// Use this for initialization
	void Start () {
		rendGame = GetComponent<Renderer>();
		rendGame.material.shader = Shader.Find("PxlShader");
		pxlTime = 0;
		time2Pxl = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (time2Pxl == 0) {
			time2Pxl = Random.Range (70, 350);
		}
		if (time2Pxl > 1) {
			time2Pxl--;
			this.rendGame.material.SetFloat("_PxlSize", 7);	
		}
		if (time2Pxl == 1 && pxlTime == 0) {
			pxlTime = Random.Range (20, 40);
		}
		if (time2Pxl == 1 && pxlTime > 0) {
			pxlTime--;
			this.rendGame.material.SetFloat("_PxlSize", 20);	
			if(pxlTime==0){
				time2Pxl=0;
			}
		}
			
	}
}
