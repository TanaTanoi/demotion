using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour {

    public float roundDuration;
    public int maxLives;
    public int maxScore;
    public float powerupFrequency;

    /**
     * Does something when a player is hit. Takes the ID number of the hitter and who was hit.
     */
    public abstract void OnHit(int playerFrom, int playerTo);
}
