using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private Vector3 original;
	public Vector3 focalPoint;
	private float zoomAmount = 0;
	private bool zooming = false;

	private float zoomSpeed = 0.08f;
	private float baseZoom = 0;
	public float desiredBaseZoom = 0;
	void Start(){
		original = transform.position;
	}
		
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (focalPoint - transform.position), 0.1f);
		transform.position = Vector3.Lerp (original, focalPoint, baseZoom + zoomAmount);
		if (zooming) {
			zoomAmount = Mathf.Min (0.4f, baseZoom + zoomAmount + zoomSpeed);
		} else {
			zoomAmount = Mathf.Max (0,  zoomAmount - zoomSpeed);
		}
		baseZoom -= (baseZoom - desiredBaseZoom) * 0.2f;
		Debug.Log (baseZoom + " " + desiredBaseZoom);
	}

	public void ZoomIn(Vector3 point){
		zooming = true;
		SetFocalPoint (point);
	}

	public void ReturnZoom(){
		zooming = false;
	}

	public void SetFocalPoint(Vector3 point){
		focalPoint = point;
	}
}
