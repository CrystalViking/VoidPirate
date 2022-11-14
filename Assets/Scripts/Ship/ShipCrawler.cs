using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrawler : MonoBehaviour
{
    public Vector2Int Position { get; set; }
    public ShipCrawler(Vector2Int startPos)
    {
        Position = startPos;
    }

    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        Direction toMove = (Direction)Random.Range(0, directionMovementMap.Count);
        Position += directionMovementMap[toMove];
        return Position;
    }
}
