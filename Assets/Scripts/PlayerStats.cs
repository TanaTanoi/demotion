using UnityEngine;
using System.Collections;

// Stats to control various aspects of player movement, items, and powerups.
[CreateAssetMenu(fileName = "Data", menuName = "Player/Stats", order = 1)]
public class PlayerStats : ScriptableObject {
    // Player stats
    public const float DEFAULT_BOOST_POWER = 450;
	public const float DEFAULT_ROTATION_SPEED = 0.09f;
	public const float DEFAULT_COOLDOWN = 0.7f;

    // Powerup stats
    // Decreases boost cooldown. Does not stack, extends duration
	public const float BOOST_DURATION = 2.0f;
	public const float BOOST_POWERUP_COOLDOWN = 0.3f;

	// Increases the boost power. Stacks and refreshes duration
	public const float POWER_DELTA = 800.0f;
	public const float POWER_DURATION = 10.0f;

    // Item stats
    public const float TOTAL_ITEM_CHARGE = 10f;

    public const float SUPER_BOOST_POWER = 1200f;
    public const float SUPER_BOOST_USES = 5f;
    public const float SUPER_BOOST_COOLDOWN = 0.7f;

    public const float THROW_USES = 5;
    public const float THROW_COOLDOWN = 0.6f;
}