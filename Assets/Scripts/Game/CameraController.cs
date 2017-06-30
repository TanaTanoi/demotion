using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private Vector3 original;
	public Vector3 focalPoint;
	public Vector3 desiredFocalPoint;
	private float zoomAmount = 0;
	private bool zooming = false;

	private Vector3 offsetDir;
	private Vector3 desiredPos;

	private float zoomSpeed = 0.08f;
	private float baseZoom = 0f;
	public float desiredBaseZoom = 0;

	private float cameraDistance = 19f;

	void Start(){
		original = transform.position;
		offsetDir = -transform.forward;
		focalPoint = original + transform.forward * 10;
	}
		
	// Update is called once per frame
	void Update () {
		// Always look at desired focal point
		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (desiredFocalPoint - transform.position), 0.4f);
		// Move towards focal point
		focalPoint += (desiredFocalPoint - focalPoint) * 0.1f;
		transform.position = Vector3.Lerp (focalPoint + (offsetDir * cameraDistance), focalPoint, baseZoom + zoomAmount);
		if (zooming) {
			zoomAmount = Mathf.Min (0.57f, baseZoom + zoomAmount + zoomSpeed); // cap at 0.5
		} else {
			zoomAmount = Mathf.Max (0,  zoomAmount - zoomSpeed);
		}

		//baseZoom -= Mathf.Clamp((baseZoom - desiredBaseZoom) * 0.2f, 0, 0.3f);
		baseZoom = desiredBaseZoom;

	}

	public void ZoomIn(Vector3 point){
		zooming = true;
		SetFocalPoint (point);
	}

	public void ReturnZoom(){
		zooming = false;
	}

	public void SetFocalPoint(Vector3 point){
		desiredFocalPoint = point;
	}
}
