using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
#if UNITY_EDITOR
        Material newMat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));
#endif
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
#if UNITY_EDITOR
        meshRenderer.material = newMat;
#endif
    }

	// Update is called once per frame
	void Update()
	{


	}
	void OnCollisionEnter(Collision col)
	{
        //psystem.Play();
        if (col.gameObject.tag == "Player") { 
			psystem.Play();
		}
	}
}