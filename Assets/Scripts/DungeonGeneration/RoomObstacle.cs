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
    int numberOfGenerationMethods = 3;
    int randomMethodNumber = Random.Range(1, numberOfGenerationMethods + 1);

    switch (randomMethodNumber)
    {
      case (1):
        obstacleList = new List<int>() { 0, 1, 14, 15, 16, 31, 96, 111, 112, 113, 126, 127 };
        break;
      case (2):
        obstacleList = new List<int>() { 32, 39, 45, 53, 60, 68, 74, 81 };
        break;
      case (3):
        obstacleList = new List<int>() { 35, 44, 50, 60, 67, 77, 83, 92 };
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
