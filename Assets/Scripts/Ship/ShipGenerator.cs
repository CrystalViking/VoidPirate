using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGenerator : MonoBehaviour
{
    public ShipGenerationData shipGenerationData;
    private List<Vector2Int> shipRooms;

    private void Start() {
        shipRooms = ShipCrawlerController.GenerateShip(shipGenerationData);
        Debug.Log("Spawning rooms");
        SpawnRooms(shipRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        ShipRoomController.instance.LoadRoom("Start", 0, 0);
        foreach(Vector2Int roomLocation in rooms)
        {
            Debug.Log(roomLocation);

            ShipRoomController.instance.LoadRoom(ShipRoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
        }
    }
}
