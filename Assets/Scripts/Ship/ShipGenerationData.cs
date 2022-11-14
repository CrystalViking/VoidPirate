using UnityEngine;

[CreateAssetMenu(fileName = "ShipGenerationData.asset", menuName = "ShipGenerationData/Ship Data")]
public class ShipGenerationData : ScriptableObject
{
    public int numberOfCrawlers;
    public int iterationMin;
    public int iterationMax;
}
