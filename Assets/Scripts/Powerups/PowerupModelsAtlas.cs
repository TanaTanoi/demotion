using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Player/PowerupAtlas", order = 2)]
public class PowerupModelsAtlas : ScriptableObject {


	/* Items. Item -> spawned item. Throwable -> thrown model */
	public GameObject bananaItem;
	public GameObject bananaThrowable;
	public GameObject snowballItem;
	public GameObject snowballThrowable;
	public GameObject fireBoostItem; // non throwable


	/* Powerups and debuffs */
	public GameObject shield;
	public GameObject coffee;

	public GameObject bananaDebuff;
	public GameObject snowballDebuff;
}
