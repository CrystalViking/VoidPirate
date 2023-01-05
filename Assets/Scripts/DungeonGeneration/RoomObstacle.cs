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
        int numberOfGenerationMethods = 10;
        int randomMethodNumber = Random.Range(1, numberOfGenerationMethods + 1);

        switch (randomMethodNumber)
        {
            case (1):
                obstacleList = new List<int>() { 115, 16, 17, 18, 79, 150, 21, 25, 56, 97, 28 };
                break;
            case (2):
                obstacleList = new List<int>() { 0, 28, 42, 56, 70, 84, 98, 112, 126, 140, 154, 168 };
                break;
            case (3):
                obstacleList = new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 51, 75, 76, 77, 78, 79, 80, 105, 129, 130, 131, 132, 133, 134, 159 };
                break;
            case (4):
                obstacleList = new List<int>() { 143, 158, 124, 128, 133, 137, 105, 109, 112, 116, 90, 91 };
                break;
            case (5):
                obstacleList = new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 75, 76, 77, 78, 79, 80, 130, 131, 132, 133, 134 };
                break;
            case (7):
                obstacleList = new List<int>() { 174, 145, 146, 154, 125, 134, 104, 105, 114, 115, 93, 66, 67, 68, 48 };
                break;
            case (8):
                obstacleList = new List<int>() { 142, 144, 155, 157, 123, 128, 136, 109, 90, 63, 71, 76, 42, 44, 55, 57 };
                break;
            case (9):
                obstacleList = new List<int>() { 173, 177, 144, 127, 135, 108, 112, 83, 90, 68, 77, 41, 46, 52, 23, 15 };
                break;
            case (10):
                obstacleList = new List<int>() { 167, 172, 141, 148, 151, 158, 122, 137, 62, 77, 41, 48, 51, 58, 27, 32 };
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
}