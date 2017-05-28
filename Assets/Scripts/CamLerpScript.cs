using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Lewis Brewer
Simple script that lerps between two points. Start and Finish.
Made for menu scene
Use two empty game objects to set start and finish.
*/
public class CamLerpScript : MonoBehaviour {
	public Transform startPoint;
	public Transform finishPoint;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
	void Start() {
		startTime = Time.time;
		journeyLength = Vector3.Distance(startPoint.position, finishPoint.position);
	}
	void Update() {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startPoint.position, finishPoint.position, fracJourney);
		transform.rotation = Quaternion.Lerp(startPoint.rotation, finishPoint.rotation, fracJourney);
	}
}
