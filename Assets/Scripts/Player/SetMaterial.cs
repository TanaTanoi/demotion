using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setMat(Material matNew)
    {
        Debug.Log("Changing Material");
        SkinnedMeshRenderer meshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        meshRenderer.material = matNew;
    }
}
