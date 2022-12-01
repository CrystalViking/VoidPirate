using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[DisallowMultipleComponent]
public class SpaceshipBuilder : SingletonMonobehaviour<SpaceshipBuilder>
{
  public Dictionary<string, SpaceshipRoom> spaceshipBuilderRoomDictionary = new Dictionary<string, SpaceshipRoom>();
  private Dictionary<string, RoomTemplateSO> roomTemplateDictionary = new Dictionary<string, RoomTemplateSO>();
  private List<RoomTemplateSO> roomTemplateList = null;
  private RoomNodeTypeListSO roomNodeTypeList;
  private bool spaceshipBuildSuccessful;

  protected override void Awake()
  {
    base.Awake();

    LoadRoomNodeTypeList();

    // For shaders is we want them in future
    //GameResources.Instance.dimmedMaterial.SetFloat("Alpha_Slider", 1f);
  }

  private void LoadRoomNodeTypeList()
  {
    roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
  }

  public bool GenerateSpaceship(SpaceshipLevelSO currentSpaceshipLevel)
  {
    roomTemplateList = currentSpaceshipLevel.roomTemplateList;

    LoadRoomTemplatesIntoDictionary();
    spaceshipBuildSuccessful = false;
    int spaceshipBuildAttempts = 0;

    while (!spaceshipBuildSuccessful && spaceshipBuildAttempts < Settings.maxSpaceshipBuildAttempts)
    {
      spaceshipBuildAttempts++;
      RoomNodeGraphSO roomNodeGraph = SelectRandomRoomNodeGraph(currentSpaceshipLevel.roomNodeGraphList);
      int spaceshipRebuildAttemptsForNodeGraph = 0;
      spaceshipBuildSuccessful = false;

      while (!spaceshipBuildSuccessful && spaceshipRebuildAttemptsForNodeGraph <= Settings.maxSpaceshipRebuildAttemptsForRoomGraph)
      {
        ClearSpaceship();
        spaceshipRebuildAttemptsForNodeGraph++;
        spaceshipBuildSuccessful = AttemptToBuildRandomSpaceship(roomNodeGraph);
      }

      if (spaceshipBuildSuccessful)
      {
        InstantiateRoomGameObjects();
      }
    }

    return spaceshipBuildSuccessful;
  }

  private void InstantiateRoomGameObjects()
  {
    foreach (KeyValuePair<string, SpaceshipRoom> keyValuePair in spaceshipBuilderRoomDictionary)
    {
      SpaceshipRoom room = keyValuePair.Value;
      int x = room.lowerBounds.x - room.templateLowerBounds.x;
      int y = room.lowerBounds.y - room.templateLowerBounds.y;
      Vector3 roomPosition = new Vector3(x, y, 0f);

      room.spawnPositionArray[0] = new Vector2Int(x, y);
      //      Debug.Log(room.roomNodeType.name + " Position: " + roomPosition);
      GameObject roomGameObject = Instantiate(room.prefab, roomPosition, Quaternion.identity, transform);
      InstantiatedRoom instantiatedRoom = roomGameObject.GetComponentInChildren<InstantiatedRoom>();
      instantiatedRoom.room = room;
      instantiatedRoom.Initialise(gameObject);
      room.instantiatedRoom = instantiatedRoom;
    }
  }



  private void LoadRoomTemplatesIntoDictionary()
  {
    roomTemplateDictionary.Clear();

    foreach (RoomTemplateSO roomTemplate in roomTemplateList)
    {
      if (!roomTemplateDictionary.ContainsKey(roomTemplate.guid))
      {
        roomTemplateDictionary.Add(roomTemplate.guid, roomTemplate);
      }
      else
      {
        Debug.Log("Duplicate Room Template Key In " + roomTemplateList);
      }
    }
  }

  private bool AttemptToBuildRandomSpaceship(RoomNodeGraphSO roomNodeGraph)
  {
    Queue<RoomNodeSO> openRoomNodeQueue = new Queue<RoomNodeSO>();
    RoomNodeSO entranceNode = roomNodeGraph.GetRoomNode(roomNodeTypeList.list.Find(x => x.isEntrance));

    if (entranceNode != null)
    {
      openRoomNodeQueue.Enqueue(entranceNode);
    }
    else
    {
      Debug.Log("No entrance node");
      return false;
    }

    bool noRoomOverlaps = true;
    noRoomOverlaps = ProcessRoomsInOpenRoomNodeQueue(roomNodeGraph, openRoomNodeQueue, noRoomOverlaps);

    if (openRoomNodeQueue.Count == 0 && noRoomOverlaps)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  private bool ProcessRoomsInOpenRoomNodeQueue(RoomNodeGraphSO roomNodeGraph, Queue<RoomNodeSO> openRoomNodeQueue, bool noRoomOverlaps)
  {
    while (openRoomNodeQueue.Count > 0 && noRoomOverlaps)
    {
      RoomNodeSO roomNode = openRoomNodeQueue.Dequeue();

      foreach (RoomNodeSO childRoomNode in roomNodeGraph.GetChildRoomNodes(roomNode))
      {
        openRoomNodeQueue.Enqueue(childRoomNode);
      }

      if (roomNode.roomNodeType.isEntrance)
      {
        RoomTemplateSO roomTemplate = GetRandomRoomTemplate(roomNode.roomNodeType);
        SpaceshipRoom room = CreateRoomFromRoomTemplate(roomTemplate, roomNode);
        room.isPositioned = true;
        spaceshipBuilderRoomDictionary.Add(room.id, room);
      }
      else
      {
        SpaceshipRoom parentRoom = spaceshipBuilderRoomDictionary[roomNode.parentRoomNodeIdList[0]];
        noRoomOverlaps = CanPlaceRoomWithNoOverlaps(roomNode, parentRoom);
      }
    }

    return noRoomOverlaps;
  }

  private bool CanPlaceRoomWithNoOverlaps(RoomNodeSO roomNode, SpaceshipRoom parentRoom)
  {
    bool roomOverlaps = true;

    while (roomOverlaps)
    {
      List<Doorway> unconnectedAvailableParentDoorways = GetUnconnectedAvailableDoorways(parentRoom.doorwayList).ToList();

      if (unconnectedAvailableParentDoorways.Count == 0)
      {
        return false;
      }

      Doorway parentDoorway = unconnectedAvailableParentDoorways.Find(x => x.orientation == roomNode.parentEntranceSide);

      // Uncomment if you want the doors to be chosen randomly
      // Doorway parentDoorway = unconnectedAvailableParentDoorways[UnityEngine.Random.Range(0, unconnectedAvailableParentDoorways.Count)];
      RoomTemplateSO roomTemplate = GetRandomTemplateForRoomConsistentWithParent(roomNode, parentDoorway);
      SpaceshipRoom room = CreateRoomFromRoomTemplate(roomTemplate, roomNode);


      if (PlaceTheRoom(parentRoom, parentDoorway, room))
      {
        roomOverlaps = false;
        room.isPositioned = true;
        spaceshipBuilderRoomDictionary.Add(room.id, room);
      }
      else
      {
        roomOverlaps = true;
      }
    }

    return true;
  }



  private RoomTemplateSO GetRandomTemplateForRoomConsistentWithParent(RoomNodeSO roomNode, Doorway parentDoorway)
  {
    RoomTemplateSO roomTemplate = null;

    if (roomNode.roomNodeType.isCorridor)
    {
      switch (parentDoorway.orientation)
      {
        case Orientation.north:
        case Orientation.south:
          roomTemplate = GetRandomRoomTemplate(roomNodeTypeList.list.Find(x => x.isCorridorNS));
          break;
        case Orientation.west:
        case Orientation.east:
          roomTemplate = GetRandomRoomTemplate(roomNodeTypeList.list.Find(x => x.isCorridorEW));
          break;
        case Orientation.none:
          break;
        default:
          break;
      }
    }
    else
    {
      roomTemplate = GetRandomRoomTemplate(roomNode.roomNodeType);
    }

    return roomTemplate;
  }

  private bool PlaceTheRoom(SpaceshipRoom parentRoom, Doorway parentDoorway, SpaceshipRoom room)
  {
    Doorway doorway = GetOppositeDoorway(parentDoorway, room.doorwayList);

    if (doorway == null)
    {
      parentDoorway.isUnavailable = true;
      return false;
    }

    Vector2Int parentDoorwayPosition = parentRoom.lowerBounds + parentDoorway.position - parentRoom.templateLowerBounds;
    Vector2Int adjustment = Vector2Int.zero;

    switch (doorway.orientation)
    {
      case Orientation.north:
        adjustment = new Vector2Int(0, -1);
        break;
      case Orientation.east:
        adjustment = new Vector2Int(-1, 0);
        break;
      case Orientation.south:
        adjustment = new Vector2Int(0, 1);
        break;
      case Orientation.west:
        adjustment = new Vector2Int(1, 0);
        break;
      case Orientation.none:
        break;
      default:
        break;
    }

    room.lowerBounds = parentDoorwayPosition + adjustment + room.templateLowerBounds - doorway.position;
    room.upperBounds = room.lowerBounds + room.templateUpperBounds - room.templateLowerBounds;
    SpaceshipRoom overlappingRoom = CheckForRoomOverlap(room);

    if (overlappingRoom == null)
    {
      parentDoorway.isConnected = true;
      parentDoorway.isUnavailable = true;
      doorway.isConnected = true;
      doorway.isUnavailable = true;

      return true;
    }
    else
    {
      parentDoorway.isUnavailable = true;

      return false;
    }
  }

  private Doorway GetOppositeDoorway(Doorway parentDoorway, List<Doorway> doorwayList)
  {
    foreach (Doorway currentDoorway in doorwayList)
    {
      if (parentDoorway.orientation == Orientation.east && currentDoorway.orientation == Orientation.west)
      {
        return currentDoorway;
      }
      else if (parentDoorway.orientation == Orientation.west && currentDoorway.orientation == Orientation.east)
      {
        return currentDoorway;
      }
      else if (parentDoorway.orientation == Orientation.north && currentDoorway.orientation == Orientation.south)
      {
        return currentDoorway;
      }
      else if (parentDoorway.orientation == Orientation.south && currentDoorway.orientation == Orientation.north)
      {
        return currentDoorway;
      }
    }

    return null;
  }

  private SpaceshipRoom CheckForRoomOverlap(SpaceshipRoom roomToCheck)
  {
    foreach (KeyValuePair<string, SpaceshipRoom> keyValuePair in spaceshipBuilderRoomDictionary)
    {
      SpaceshipRoom room = keyValuePair.Value;
      if (room.id == roomToCheck.id || !room.isPositioned)
      {
        continue;
      }

      if (IsOverlappingRoom(roomToCheck, room))
      {
        return room;
      }
    }

    return null;
  }

  private bool IsOverlappingRoom(SpaceshipRoom room1, SpaceshipRoom room2)
  {
    bool isOverlappingX = IsOverlappingInterval(room1.lowerBounds.x, room1.upperBounds.x, room2.lowerBounds.x, room2.upperBounds.x);
    bool isOverlappingY = IsOverlappingInterval(room1.lowerBounds.y, room1.upperBounds.y, room2.lowerBounds.y, room2.upperBounds.y);

    return isOverlappingX && isOverlappingY;
  }

  private bool IsOverlappingInterval(int imin1, int imax1, int imin2, int imax2)
  {
    return Mathf.Max(imin1, imin2) <= Mathf.Min(imax1, imax2);
  }

  private RoomTemplateSO GetRandomRoomTemplate(RoomNodeTypeSO roomNodeType)
  {
    List<RoomTemplateSO> matchingRoomTemplateList = new List<RoomTemplateSO>();
    foreach (RoomTemplateSO roomTemplate in roomTemplateList)
    {
      if (roomTemplate.roomNodeType == roomNodeType)
      {
        matchingRoomTemplateList.Add(roomTemplate);
      }
    }

    if (matchingRoomTemplateList.Count == 0)
    {
      return null;
    }

    return matchingRoomTemplateList[UnityEngine.Random.Range(0, matchingRoomTemplateList.Count)];
  }

  private IEnumerable<Doorway> GetUnconnectedAvailableDoorways(List<Doorway> doorwayList)
  {
    foreach (Doorway doorway in doorwayList)
    {
      if (!doorway.isConnected && !doorway.isUnavailable)
      {
        yield return doorway;
      }
    }
  }

  private SpaceshipRoom CreateRoomFromRoomTemplate(RoomTemplateSO roomTemplate, RoomNodeSO roomNode)
  {
    SpaceshipRoom room = new SpaceshipRoom();
    room.roomTemplateId = roomTemplate.guid;
    room.id = roomNode.id;
    room.prefab = roomTemplate.prefab;
    room.roomNodeType = roomTemplate.roomNodeType;
    room.lowerBounds = roomTemplate.lowerBounds;
    room.upperBounds = roomTemplate.upperBounds;
    room.spawnPositionArray = roomTemplate.spawnPositionArray;
    room.enemiesByLevelList = roomTemplate.enemiesByLevelList;
    room.roomLevelEnemySpawnParametersList = roomTemplate.roomEnemySpawnParametersList;
    room.templateLowerBounds = roomTemplate.lowerBounds;
    room.templateUpperBounds = roomTemplate.upperBounds;
    room.childRoomIdList = CopyStringList(roomNode.childRoomNodeIdList);
    room.doorwayList = CopyDoorwayList(roomTemplate.doorwayList);


    if (roomNode.parentRoomNodeIdList.Count == 0) // Only entrance has no parent
    {
      room.parentRoomId = "";
      room.isPreviouslyVisited = true;
      SpaceshipGameManager.Instance.SetCurrentRoom(room);

    }
    else
    {
      room.parentRoomId = roomNode.parentRoomNodeIdList[0];
    }

    if (room.GetNumberOfEnemiesToSpawn(SpaceshipGameManager.Instance.GetCurrentSpaceshipLevel()) == 0)
    {
      room.isClearedOfEnemies = true;
    }

    return room;

  }

  private RoomNodeGraphSO SelectRandomRoomNodeGraph(List<RoomNodeGraphSO> roomNodeGraphList)
  {
    if (roomNodeGraphList.Count > 0)
    {
      return roomNodeGraphList[UnityEngine.Random.Range(0, roomNodeGraphList.Count)];
    }
    else
    {
      Debug.Log("No room node graphs in the list");
      return null;
    }
  }

  private List<Doorway> CopyDoorwayList(List<Doorway> oldDoorwayList)
  {
    List<Doorway> newDoorwayList = new List<Doorway>();

    foreach (Doorway doorway in oldDoorwayList)
    {
      Doorway newDoorway = new Doorway();
      newDoorway.position = doorway.position;
      newDoorway.orientation = doorway.orientation;
      newDoorway.doorPrefab = doorway.doorPrefab;
      newDoorway.isConnected = doorway.isConnected;
      newDoorway.isUnavailable = doorway.isUnavailable;
      newDoorway.doorwayStartCopyPos = doorway.doorwayStartCopyPos;
      newDoorway.doorwayCopyTileWidth = doorway.doorwayCopyTileWidth;
      newDoorway.doorwayCopyTileHeight = doorway.doorwayCopyTileHeight;

      newDoorwayList.Add(newDoorway);
    }

    return newDoorwayList;
  }

  private List<string> CopyStringList(List<string> oldStringList)
  {
    List<string> newStringList = new List<string>();

    foreach (string stringValue in oldStringList)
    {
      newStringList.Add(stringValue);
    }

    return newStringList;
  }

  public RoomTemplateSO GetRoomTemplate(string roomTemplateId)
  {
    if (roomTemplateDictionary.TryGetValue(roomTemplateId, out RoomTemplateSO roomTemplate))
    {
      return roomTemplate;
    }
    else
    {
      return null;
    }
  }

  public SpaceshipRoom GetRoomByRoomId(string roomId)
  {
    if (spaceshipBuilderRoomDictionary.TryGetValue(roomId, out SpaceshipRoom room))
    {
      return room;
    }
    else
    {
      return null;
    }
  }

  private void ClearSpaceship()
  {
    if (spaceshipBuilderRoomDictionary.Count > 0)
    {
      foreach (KeyValuePair<string, SpaceshipRoom> keyValuePair in spaceshipBuilderRoomDictionary)
      {
        SpaceshipRoom room = keyValuePair.Value;

        if (room.instantiatedRoom != null)
        {
          Destroy(room.instantiatedRoom.gameObject);
        }
      }
      spaceshipBuilderRoomDictionary.Clear();
    }
  }
}
