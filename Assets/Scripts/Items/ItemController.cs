using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Pickupable {

    public Item.Type type;

	private const int SPIN_SPEED = 50;

	// Use this for initialization
	void Start () {
		
	}

	void Update(){
		Spin ();
	}

	// Occurs when this object is picked up by a player.
	// Used to control grab audio or visual
	public void Pickup(GameObject pickuper) {
        ShrinkTowardsAndDestory(pickuper.transform);
	}

	public void Spin(){
		transform.rotation = Quaternion.Euler (new Vector3 (0, (Time.time * SPIN_SPEED) % 360, 0));
	}
}
