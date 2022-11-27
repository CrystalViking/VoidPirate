using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnableObjects<T>
{
  private struct chanceBoundaries
  {
    public T spawnableObject;
    public int lowBoundaryValue;
    public int highBoundaryValue;
  }

  private int ratioValueTotal;
  private List<chanceBoundaries> chanceBoundariesList = new List<chanceBoundaries>();
  private List<SpawnableObjectsByLevel<T>> spawnableObjectsByLevelList;

  public RandomSpawnableObjects(List<SpawnableObjectsByLevel<T>> spawnableObjectsByLevelList)
  {
    this.spawnableObjectsByLevelList = spawnableObjectsByLevelList;
  }
}
