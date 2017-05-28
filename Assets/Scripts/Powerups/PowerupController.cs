using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : Pickupable {

	public Powerup.Type type;

	// Occurs when this object is picked up by a player.
	// Used to control grab audio or visual
	public void Pickup(GameObject pickuper) {
        ShrinkTowardsAndDestory(pickuper.transform);
	}
}
