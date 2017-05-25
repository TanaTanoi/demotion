using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairTrails : MonoBehaviour {
    List<Vector3> trailPoints = new List<Vector3>();
    public GameObject prefab;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        trailPoints.Add(transform.position);
        for(int i=0;i<trailPoints.Count-1;i++)
        {
            Debug.DrawLine(trailPoints[i], trailPoints[i + 1], Color.red);
           // Instantiate(prefab, trailpos, Quaternion.identity);
        }
	}
}
