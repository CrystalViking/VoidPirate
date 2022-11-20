using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceshipLevel_", menuName = "Scriptable Objects/Ship/Spaceship Level")]
public class SpaceshipLevelSO : ScriptableObject
{
  [Space(10)]
  [Header("Basic level details")]
  [Tooltip("Level name")]
  public string levelName;
  [Space(10)]
  [Header("Room templates for level ")]
  [Tooltip("You need to ensure that room templates are included for all room node types for Room Node Graph for the level")]
  public List<RoomTemplateSO> roomTemplateList;
  [Space(10)]
  [Header("Room Node Graphs for level")]
  [Tooltip("Populate this list with the Room Node Graphs which should be randomly selected from for the level")]
  public List<RoomNodeGraphSO> roomNodeGraphList;

#if UNITY_EDITOR

  private void OnValidate()
  {

    if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomTemplateList), roomTemplateList) ||
    HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomNodeGraphList), roomNodeGraphList) ||
     HelperUtilities.ValidateCheckEmptyString(this, nameof(levelName), levelName))
    {
      return;
    }

    bool isEWCorridor = false;
    bool isNSCorridor = false;
    bool isEntrance = false;

    foreach (RoomTemplateSO roomTemplate in roomTemplateList)
    {
      if (roomTemplate == null)
      {
        return;
      }

      if (roomTemplate.roomNodeType.isCorridorEW)
      {
        isEWCorridor = true;
      }

      if (roomTemplate.roomNodeType.isCorridorNS)
      {
        isNSCorridor = true;
      }

      if (roomTemplate.roomNodeType.isEntrance)
      {
        isEntrance = true;
      }
    }

    if (isEWCorridor == false)
    {
      Debug.Log("In " + this.name.ToString() + ": No EW Corridor Room Type Specified");
    }

    if (isNSCorridor == false)
    {
      Debug.Log("In " + this.name.ToString() + ": No NS Corridor Room Type Specified");
    }

    if (isEntrance == false)
    {
      Debug.Log("In " + this.name.ToString() + ": No Entrance Room Type Specified");
    }

    foreach (RoomNodeGraphSO roomNodeGraph in roomNodeGraphList)
    {
      if (roomNodeGraph == null)
      {
        return;
      }

      foreach (RoomNodeSO roomNode in roomNodeGraph.roomNodeList)
      {
        if (roomNode == null)
        {
          continue;
        }

        if (roomNode.roomNodeType.isEntrance || roomNode.roomNodeType.isCorridorEW ||
        roomNode.roomNodeType.isCorridor || roomNode.roomNodeType.isCorridorNS ||
        roomNode.roomNodeType.isNone)
        {
          continue;
        }

        bool isRoomNodeTypeFound = false;

        foreach (RoomTemplateSO roomTemplate in roomTemplateList)
        {
          if (roomTemplate == null)
          {
            continue;
          }

          if (roomTemplate.roomNodeType == roomNode.roomNodeType)
          {
            isRoomNodeTypeFound = true;
            break;
          }
        }

        if (!isRoomNodeTypeFound)
        {
          Debug.Log("In " + this.name.ToString() + ": No Room Template " +
          roomNode.roomNodeType.name.ToString() + " found for node graph: " + roomNodeGraph.name.ToString());
        }
      }
    }

  }
#endif
}
