﻿using UnityEngine;
using System.Collections;

// Stats to control various aspects of player movement, items, and powerups.
[CreateAssetMenu(fileName = "Data", menuName = "Player/Stats", order = 1)]
public class PlayerStats : ScriptableObject {
    /* Player stats */
    public float DEFAULT_BOOST_POWER = 450;
	public float DEFAULT_ROTATION_SPEED = 5f;
	public float DEFAULT_COOLDOWN = 0.7f;
	public float DEFAULT_MAX_BOOST_HOLD_TIME = 0.5f;
	public float MINIMUM_BOOST_POWER = 200;
	public AnimationCurve BOOST_POWER_RAMP;

    /* Powerup stats */
    // Decreases boost cooldown. Does not stack, extends duration
	public float BOOST_DURATION = 2.0f;
	public float BOOST_POWERUP_COOLDOWN = 0.3f;

	public float SHIELD_DURATION = 4f;
	public float SHIELD_PUSH_POWER = 50f;

	// Increases the boost power. Stacks and refreshes duration
	public float POWER_DELTA = 800.0f;
	public float POWER_DURATION = 10.0f;

    /* Item stats */
    public float TOTAL_ITEM_CHARGE = 10f;

    public float SUPER_BOOST_POWER = 1200f;
    public float SUPER_BOOST_USES = 5f;
    public float SUPER_BOOST_COOLDOWN = 0.7f;

    public float THROW_USES = 1;
    public float THROW_COOLDOWN = 0.6f;

	public float STICKY_DURATION = 3.0f;
	public float STICKY_POWERDOWN_ROTATION_SPEED = 2f;

	public float BANANA_POWER = 100f;
}