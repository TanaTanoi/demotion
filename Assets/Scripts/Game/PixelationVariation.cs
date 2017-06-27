using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelationVariation : MonoBehaviour {
	public Renderer rendGame;
	// Use this for initialization
	void Start () {
		rendGame = GetComponent<Renderer>();
		rendGame.material.shader = Shader.Find("Pxl");
	}
	
	// Update is called once per frame
	void Update () {
		this.rendGame.material.SetFloat("_Pxlation", 2);	
	}
}
