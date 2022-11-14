using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrawlerController : MonoBehaviour
{

    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.top, Vector2Int.up},
        {Direction.left, Vector2Int.left},
        {Direction.down, Vector2Int.down},
        {Direction.right, Vector2Int.right}
    };

    public static List<Vector2Int> GenerateShip(ShipGenerationData dungeonData)
    {
        List<ShipCrawler> dungeonCrawlers = new List<ShipCrawler>();
        for (int i = 0; i < dungeonData.numberOfCrawlers; i++)
        {
            dungeonCrawlers.Add(new ShipCrawler(Vector2Int.zero));
        }

        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);

        for (int i = 0; i < iterations; i++)
        {
            foreach (ShipCrawler dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }

        return positionsVisited;
    }
}
