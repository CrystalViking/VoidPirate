using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipRoom
{
  public string id;
  public string roomTemplateId;
  public GameObject prefab;
  public RoomNodeTypeSO roomNodeType;
  public Vector2Int lowerBounds;
  public Vector2Int upperBounds;
  public Vector2Int templateLowerBounds;
  public Vector2Int templateUpperBounds;
  public Vector2Int[] spawnPositionArray;
  public List<SpawnableObjectsByLevel<EnemyDetailsSO>> enemiesByLevelList;
  public List<RoomEnemySpawnParameters> roomLevelEnemySpawnParametersList;
  public List<string> childRoomIdList;
  public string parentRoomId;
  public List<Doorway> doorwayList;
  public bool isPositioned = false;
  public InstantiatedRoom instantiatedRoom;
  public bool isLit = false;
  public bool isClearedOfEnemies = false;
  public bool isPreviouslyVisited = false;

  public void Room()
  {
    childRoomIdList = new List<string>();
    doorwayList = new List<Doorway>();
  }


  public int GetNumberOfEnemiesToSpawn(SpaceshipLevelSO spaceshipLevel)
  {
    foreach (RoomEnemySpawnParameters roomEnemySpawnParameters in roomLevelEnemySpawnParametersList)
    {
      if (roomEnemySpawnParameters.spaceshipLevel == spaceshipLevel)
      {
        return Random.Range(roomEnemySpawnParameters.minTotalEnemiesToSpawn, roomEnemySpawnParameters.maxTotalEnemiesToSpawn);
      }
    }

    return 0;
  }

  public RoomEnemySpawnParameters GetRoomEnemySpawnParameters(SpaceshipLevelSO spaceshipLevel)
  {
    foreach (RoomEnemySpawnParameters roomEnemySpawnParameters in roomLevelEnemySpawnParametersList)
    {
      if (roomEnemySpawnParameters.spaceshipLevel == spaceshipLevel)
      {
        return roomEnemySpawnParameters;
      }
    }
    return null;
  }
}
