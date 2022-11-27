using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
  public static Camera mainCamera;

  public static Vector3 GetMouseWorldPosition()
  {
    if (mainCamera == null)
    {
      mainCamera = Camera.main;
    }

    Vector3 mouseScreenPosition = Input.mousePosition;

    mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
    mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

    Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

    worldPosition.z = 0f;

    return worldPosition;

  }
  public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck)
  {
    if (stringToCheck == "")
    {
      Debug.Log(fieldName + " is empty and must contain a value in object " + thisObject.name.ToString());
      return true;
    }
    return false;
  }

  public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck)
  {
    bool error = false;
    int count = 0;

    if (enumerableObjectToCheck == null)
    {
      Debug.Log(fieldName + " is null in object " + thisObject.name.ToString());
      return true;
    }

    foreach (var item in enumerableObjectToCheck)
    {
      if (item == null)
      {
        Debug.Log(fieldName + " has null values in object " + thisObject.name.ToString());
        error = true;
      }
      else
      {
        count++;
      }
    }

    if (count == 0)
    {
      Debug.Log(fieldName + " has no values in object " + thisObject.name.ToString());
      error = true;
    }

    return error;
  }

  public static bool ValidateCheckNullValue(Object thisObject, string fieldName, UnityEngine.Object objectToCheck)
  {
    if (objectToCheck == null)
    {
      Debug.Log(fieldName + " is null and must contain a value in object " + thisObject.name.ToString());
      return true;
    }
    return false;
  }

  public static Vector3 GetSpawnPositionNearestToPlayer(Vector3 playerPosition)
  {
    SpaceshipRoom currentRoom = SpaceshipGameManager.Instance.GetCurrentRoom();

    Grid grid = currentRoom.instantiatedRoom.grid;

    Vector3 nearestSpawnPosition = new Vector3(10000f, 10000f, 0f);

    foreach (Vector2Int spawnPositionGrid in currentRoom.spawnPositionArray)
    {
      Vector3 spawnPositionWorld = grid.CellToWorld((Vector3Int)spawnPositionGrid);

      if (Vector3.Distance(spawnPositionWorld, playerPosition) < Vector3.Distance(nearestSpawnPosition, playerPosition))
      {
        nearestSpawnPosition = spawnPositionWorld;
      }
    }

    return nearestSpawnPosition;

  }
}
