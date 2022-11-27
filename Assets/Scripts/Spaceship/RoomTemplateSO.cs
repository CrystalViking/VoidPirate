using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Room_", menuName = "Scriptable Objects/Ship/Room")]
public class RoomTemplateSO : ScriptableObject
{
  [HideInInspector]
  public string guid;

  [Space(10)]
  [Header("Room prefab")]
  [Tooltip("The gameobject prefab for the room (this will contain all the tilemaps for the room and environment game objects")]
  public GameObject prefab;
  [HideInInspector]
  public GameObject previousPrefab;
  [Space(10)]
  [Header("Room configuration")]
  public RoomNodeTypeSO roomNodeType;
  [Tooltip("The bottom left corner of the room")]
  public Vector2Int lowerBounds;
  [Tooltip("The top right corner of the room")]
  public Vector2Int upperBounds;
  [Tooltip("Max 4 doorways for a room - E, W, N and S. Each doorway should have 3 tile width opening. Middle tile position is the doorway coordinate 'position'")]
  [SerializeField]
  [NonReorderable]
  public List<Doorway> doorwayList;
  [Tooltip("Each possible spawn position (used for enemies and items) for the room in the tilemap coordinates should be added to this array")]
  public Vector2Int[] spawnPositionArray;
  [Space(10)]
  [Header("Enemy details")]
  [NonReorderable]
  public List<SpawnableObjectsByLevel<EnemyDetailsSO>> enemiesByLevelList;
  [NonReorderable]
  public List<RoomEnemySpawnParameters> roomEnemySpawnParametersList;


  public List<Doorway> GetDoorwayList()
  {
    return doorwayList;
  }

#if UNITY_EDITOR
  private void OnValidate()
  {
    // Set unique GUID if empty or the prefab changes
    if (guid == "" || previousPrefab != prefab)
    {
      guid = GUID.Generate().ToString();
      previousPrefab = prefab;
      EditorUtility.SetDirty(this);
    }
    HelperUtilities.ValidateCheckNullValue(this, nameof(prefab), prefab);
    HelperUtilities.ValidateCheckNullValue(this, nameof(roomNodeType), roomNodeType);

    HelperUtilities.ValidateCheckEnumerableValues(this, nameof(doorwayList), doorwayList);

    // Check enemies and room spawn parameters for levels
    if (enemiesByLevelList.Count > 0 || roomEnemySpawnParametersList.Count > 0)
    {
      HelperUtilities.ValidateCheckEnumerableValues(this, nameof(enemiesByLevelList), enemiesByLevelList);
      HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomEnemySpawnParametersList), roomEnemySpawnParametersList);

      foreach (RoomEnemySpawnParameters roomEnemySpawnParameters in roomEnemySpawnParametersList)
      {
        HelperUtilities.ValidateCheckNullValue(this, nameof(roomEnemySpawnParameters.spaceshipLevel), roomEnemySpawnParameters.spaceshipLevel);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(roomEnemySpawnParameters.minTotalEnemiesToSpawn), roomEnemySpawnParameters.minTotalEnemiesToSpawn, nameof(roomEnemySpawnParameters.maxTotalEnemiesToSpawn), roomEnemySpawnParameters.maxTotalEnemiesToSpawn, true);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(roomEnemySpawnParameters.minSpawnInterval), roomEnemySpawnParameters.minSpawnInterval, nameof(roomEnemySpawnParameters.maxSpawnInterval), roomEnemySpawnParameters.maxSpawnInterval, true);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(roomEnemySpawnParameters.minConcurrentEnemies), roomEnemySpawnParameters.minConcurrentEnemies, nameof(roomEnemySpawnParameters.maxConcurrentEnemies), roomEnemySpawnParameters.maxConcurrentEnemies, false);

        bool isEnemyTypesListForDungeonLevel = false;

        foreach (SpawnableObjectsByLevel<EnemyDetailsSO> spaceshipObjectsByLevel in enemiesByLevelList)
        {
          if (spaceshipObjectsByLevel.spaceshipLevel == roomEnemySpawnParameters.spaceshipLevel && spaceshipObjectsByLevel.spawnableObjectRatioList.Count > 0)
            isEnemyTypesListForDungeonLevel = true;

          HelperUtilities.ValidateCheckNullValue(this, nameof(spaceshipObjectsByLevel.spaceshipLevel), spaceshipObjectsByLevel.spaceshipLevel);

          foreach (SpawnableObjectRatio<EnemyDetailsSO> spaceshipObjectRatio in spaceshipObjectsByLevel.spawnableObjectRatioList)
          {
            HelperUtilities.ValidateCheckNullValue(this, nameof(spaceshipObjectRatio.spaceshipObject), spaceshipObjectRatio.spaceshipObject);

            HelperUtilities.ValidateCheckPositiveValue(this, nameof(spaceshipObjectRatio.ratio), spaceshipObjectRatio.ratio, false);
          }

        }

        if (isEnemyTypesListForDungeonLevel == false && roomEnemySpawnParameters.spaceshipLevel != null)
        {
          Debug.Log("No enemy types specified in for dungeon level " + roomEnemySpawnParameters.spaceshipLevel.levelName + " in gameobject " + this.name.ToString());
        }
      }
    }

    HelperUtilities.ValidateCheckEnumerableValues(this, nameof(spawnPositionArray), spawnPositionArray);
  }
#endif
}
