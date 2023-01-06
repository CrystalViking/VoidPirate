using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour

{
    public static RoomController instance;
    string currentWorldName = "Dungeon";
    RoomInfo currentLoadRoomData;
    Room currRoom;
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();
    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;
    private bool shouldActiveEnemy = true;


    private void Start()
    {
        // LoadRoom("Test", 0, 0);
        // LoadRoom("Test", 1, 0);
        // LoadRoom("Test", -1, 0);
        // LoadRoom("Test", -2, 0);
    }

    private void Update()
    {
        UpdateRoomQueue();
        CheckIfCanOpenBossRoom();
    }

    private void CheckIfCanOpenBossRoom()
    {
        if (AreEnoughRoomsCleared())
        {
            if (loadRoomQueue.Count == 0)
            {
                if (!spawnedBossRoom)
                {
                    StartCoroutine(SpawnBossRoom());
                }
                else if (spawnedBossRoom && !updatedRooms)
                {
                    foreach (Room room in loadedRooms)
                    {
                        room.RemoveUnconnectedDoors();
                        room.AddMissingDoors();
                    }

                    UpdateRooms();
                    updatedRooms = true;
                }
                return;
            }
        }
    }

    private void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            // if (!spawnedBossRoom)
            // {
            //     StartCoroutine(SpawnBossRoom());
            // }
            // else if (spawnedBossRoom && !updatedRooms)
            // {
            foreach (Room room in loadedRooms)
            {
                room.RemoveUnconnectedDoors();
            }

            UpdateRooms();
            //     updatedRooms = true;
            // }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.0f);
        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Vector2Int tempRoom = new Vector2Int(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.x && r.Y == tempRoom.y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.x, tempRoom.y);
        }
    }

    void Awake()
    {
        instance = this;
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            Debug.Log("Room already exists.");
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (!loadRoom.isDone)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height, 0);
            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }


    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[]
        {
            //"Empty",
            "Basic1"
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;


        StartCoroutine(RoomCorutine());
    }

    public IEnumerator RoomCorutine()
    {
        //yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.0f);
        shouldActiveEnemy = true;
        UpdateRooms();
    }

    private bool AreEnoughRoomsCleared()
    {
        int totalRoomCount = 0;
        int clearedRoomCount = 0;

        foreach (Room room in loadedRooms)
        {
            totalRoomCount++;
            bool areAllEnemiesDead = true;

            List<IEnemy> enemies = new List<IEnemy>(room.GetComponentsInChildren<IEnemy>());
            if (enemies.Count > 0)
            {
                foreach (IEnemy enemy in enemies)
                {
                    if (enemy.GetEnemyState() != EnemyState.Die)
                    {
                        areAllEnemiesDead = false;
                    }
                }
            }

            if (areAllEnemiesDead)
            {
                clearedRoomCount++;
            }
        }
        //Debug.Log((clearedRoomCount > (totalRoomCount / 2)));
        //Debug.Log(clearedRoomCount + ", " + totalRoomCount);
        return clearedRoomCount > (totalRoomCount / 2);
    }

    public void UpdateRooms()
    {
        foreach (Room room in loadedRooms)
        {
            bool areAllEnemiesDead = true;

            List<IEnemy> enemies = new List<IEnemy>(room.GetComponentsInChildren<IEnemy>());

            if (enemies.All(x => x.GetEnemyState() == EnemyState.Die))
            {
                enemies.Clear();
            }

            if (currRoom != room)
            {
                if (enemies != null)
                {
                    foreach (IEnemy enemy in enemies)
                    {
                        enemy.SetActiveBehaviourFalse();
                    }
                }

                foreach (Door door in room.GetComponentsInChildren<Door>())
                {
                    door.doorCollider.enabled = false;
                    door.OpenDoor();
                }
            }
            else
            {

                if (enemies.Count > 0)
                {
                    foreach (IEnemy enemy in enemies)
                    {
                        if (enemy.GetEnemyState() != EnemyState.Die)
                        {
                            areAllEnemiesDead = false;
                        }
                    }
                }

                //enemies.Length > 0
                if (!areAllEnemiesDead)
                {
                    if (shouldActiveEnemy)
                    {
                        foreach (IEnemy enemy in enemies)
                        {
                            enemy.SetActiveBehaviourTrue();
                        }
                        shouldActiveEnemy = false;
                    }

                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.enabled = true;
                        door.CloseDoor();
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        switch (door.doorType)
                        {
                            case Door.DoorType.left:
                                break;
                        }
                        door.doorCollider.enabled = false;
                        door.OpenDoor();
                    }
                }
            }
        }
    }
}
