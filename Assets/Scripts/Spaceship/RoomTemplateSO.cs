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

  public List<Doorway> GetDoorwayList()
  {
    return doorwayList;
  }

#if UNITY_EDITOR
  private void OnValidate()
  {
    if (guid == "" || previousPrefab != prefab)
    {
      guid = GUID.Generate().ToString();
      previousPrefab = prefab;
      EditorUtility.SetDirty(this);
    }

    HelperUtilities.ValidateCheckEnumerableValues(this, nameof(doorwayList), doorwayList);
    HelperUtilities.ValidateCheckEnumerableValues(this, nameof(spawnPositionArray), spawnPositionArray);
  }
#endif
}
