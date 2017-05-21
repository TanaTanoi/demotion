using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameSettings {
    public Dictionary<int, InputType> IDtoInput;
    public int playerCount;
    public GameSetup.GameMode mode;
    public int numberRounds;
    public float roundDuration;
    public float respawnTime;
    public int maxLives;
    public int targetScore;
    public int targetKills;
    
}
