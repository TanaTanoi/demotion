using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject {
    public Dictionary<int, InputType> IDtoInput;
    public int playerCount = 2;
	public GameSetup.GameMode mode = GameSetup.GameMode.DEMOTION;
    public int numberRounds = 2;
    public float roundDuration = 60.0f;
    public float respawnTime = 5.0f;
    public int maxLives = 10;
    public int targetScore = 10000;
    public int targetKills = 20;
    
}
