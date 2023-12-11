using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Starting,
    Playing,
    Gameover
}    
public enum GameTag
{
    Platform,
    Player,
    LeftCorner,
    RightCorner,
    Collectable
}

public enum PrefKey
{
    BestScore,
    CheckType,
    NumberOfCoins,
    Player_,
    Cur_player_id,
    Selected_character_id,
    View_character_id
}

[System.Serializable]
public class CollectableItem
{
    public Collectable collectablePrefabs;
    [Range(0f, 1f)]
    public float spawnRate;
}