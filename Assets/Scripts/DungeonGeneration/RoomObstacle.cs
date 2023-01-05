using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObstacle : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }
    public GridController grid;
    public RandomSpawner spawner;
    private List<int> obstacleList = new List<int>();

    private void Start()
    {

    }

    public void SpawnObstacles()
    {
        int numberOfGenerationMethods = 8;
        int randomMethodNumber = Random.Range(1, numberOfGenerationMethods + 1);

        switch (randomMethodNumber)
        {
            case (1):
                obstacleList = new List<int>() { 115, 16, 17, 18, 79, 150, 21, 25, 56, 97, 28, };
                break;
            case (2):
                obstacleList = new List<int>() { 0, 28, 42, 56, 70, 84, 98, 112, 126, 140, 154, 168, };
                break;
            case (3):
                obstacleList = new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 51, 75, 76, 77, 78, 79, 80, 105, 129, 130, 131, 132, 133, 134, 159, };
                break;
            case (5):
                obstacleList = new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 75, 76, 77, 78, 79, 80, 130, 131, 132, 133, 134, };
                break;
            default:
                obstacleList = GenerateRandomIndexes();
                break;

        }

        foreach (int pos in obstacleList)
        {
            GameObject go = Instantiate(spawner.spawnerData.itemToSpawn, grid.availablePoints[pos], Quaternion.identity, transform) as GameObject;
        }

        for (int i = obstacleList.Count - 1; i >= 0; i--)
        {
            grid.availablePoints.RemoveAt(obstacleList[i]);
        }
    }

    public List<int> GenerateRandomIndexes()
    {
        // Create a list to store the random indexes
        List<int> indexes = new List<int>();

        // Create a list of the indices of the red boxes
        List<int> redBoxIndices = new List<int> { 81, 100, 101, 120, 190, 191, 10, 11 };

        // Generate 3 random indexes in the range from 41 to 160
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(41, 161);
            if (!redBoxIndices.Contains(index))
            {
                indexes.Add(index);
            }
            else
            {
                // Decrement the loop counter to compensate for the skipped index
                i--;
            }
        }

        // Return the list of indexes
        return indexes;
    }
}