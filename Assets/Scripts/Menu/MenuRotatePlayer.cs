using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotatePlayer : MonoBehaviour {
	void Update()
	{
		transform.Rotate(Vector3.up * Time.deltaTime * 15, Space.World);
	}
}
