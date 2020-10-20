using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room Configuration")]
public class RoomConfiguration : ScriptableObject
{
    public int MaxPlayers;
    public Vector2 StartingPositionMin;
    public Vector2 StartingPositionMax;

    public int ScoreForEnemyDestroyed;
    public int ScoreForDestructableDestroyed;
}
