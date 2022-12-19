using UnityEngine;

[CreateAssetMenu(fileName ="Spawner.asset", menuName ="Spawners/Spawner")]
public class SpawnerData : ScriptableObject
{
    public string nameOfGroup;
    public GameObject itemToSpawn;
    public int minSpawn;
    public int maxSpawn;  
}
