using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Game/Settings", order = 1)]
public class GameSettings : ScriptableObject {
    public Dictionary<int, InputType> IDtoInput;
    public int playerCount = 2;
    public GameSetup.GameMode mode = GameSetup.GameMode.DEMOTION;
    public int numberRounds = 3;
    public float roundDuration = 60.0f;
    public float respawnTime = 3f;
    public int maxLives = 5;
    public int targetScore = 5000;
    public int targetKills = 20;
    
}
