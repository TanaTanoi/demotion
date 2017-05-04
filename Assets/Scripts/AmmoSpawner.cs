using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour {

    public GameObject ItemToSpawn;

    private GameObject ammo;
    private bool objectActive = true;
	// Use this for initialization
	void Start () {
        CreateBox(0);
        InvokeRepeating("CheckBox", 2, 2.5f);
	}
	
	void CheckBox() {
        if(ammo == null) {
            CreateBox(2);
        }
	}

    private void CreateBox(float delay) {
        IEnumerator c = CreateBoxWithDelayRoutine(delay);
        StartCoroutine(c);
    }

    private IEnumerator CreateBoxWithDelayRoutine(float delay) {
        yield return new WaitForSeconds(delay);
        ammo = Instantiate<GameObject>(ItemToSpawn);
        ammo.transform.position = transform.position;
    }
}
