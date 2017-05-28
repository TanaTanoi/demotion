using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class storageMaterial : MonoBehaviour
{
	private ParticleSystem psystem;
	private bool coltrack = false;
	// Use this for initialization
	void Start()
	{
		psystem = GetComponentInChildren<ParticleSystem>();
		int rand = Random.Range(0, 3);
		string Customization = "STORAGERED";
		if (rand == 1)
		{
			Customization = "STORAGEBLUE";
		}
		else if (rand == 2)
		{
			Customization = "STORAGEGREEN";
		}
		else if (rand == 3)
		{
			Customization = "STORAGERED";
		}
		string matPath = "Assets/Materials&Textures/Storage/" + Customization + ".mat";
		Material newMat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));
		MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
		meshRenderer.material = newMat;
	}

	// Update is called once per frame
	void Update()
	{


	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player") { 
			//psystem.Play();
		}
	}
}