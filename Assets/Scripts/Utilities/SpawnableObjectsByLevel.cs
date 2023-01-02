using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObjectsByLevel<T>
{
  public SpaceshipLevelSO spaceshipLevel;
  [NonReorderable]
  public List<SpawnableObjectRatio<T>> spawnableObjectRatioList;
}
