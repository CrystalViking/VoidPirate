using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SpaceshipGameManager : SingletonMonobehaviour<SpaceshipGameManager>
{
    [Space(10)]
    [Header("Spaceship levels")]
    [Tooltip("Populate with the spaceship level SO")]
    [SerializeField]
    [NonReorderable]
    private List<SpaceshipLevelSO> spaceshipLevelList;
    [Tooltip("Populate with the starting spaceship level for testing, first level = 0")]
    [SerializeField]
    private int currentSpaceshipListIndex = 0;
    [SerializeField]
    [HideInInspector]
    public GameState gameState;
    [HideInInspector] public GameState previousGameState;
    private SpaceshipRoom currentRoom;
    private SpaceshipRoom previousRoom;
    Timer timer;

    void Start()
    {
        previousGameState = GameState.gameStarted;
        gameState = GameState.gameStarted;
        timer = GetComponent<Timer>();
    }

    void Update()
    {
        HandleGameState();



        // TO DELETE
        if (Input.GetKeyDown(KeyCode.F8))
        {
            gameState = GameState.gameStarted;
        }
    }

    void DisableTimer()
    {
        timer.enabled = false;
    }

    void EnableTimer()
    {
        timer.enabled = true;
    }

    private void OnEnable()
    {
        StaticEventHandler.OnRoomChanged += StaticEventHandler_OnRoomChanged;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnRoomChanged -= StaticEventHandler_OnRoomChanged;
    }

    private void StaticEventHandler_OnRoomChanged(RoomChangedEventArgs roomChangedEventArgs)
    {
        SetCurrentRoom(roomChangedEventArgs.room);
    }

    private void HandleGameState()
    {
        switch (gameState)
        {
            case GameState.gameStarted:
                if (currentSpaceshipListIndex == 1)
                {
                    DisableTimer();
                    GameResources.Instance.litMaterial.SetFloat("Alpha_Slider", 1);

                }
                else
                {
                    EnableTimer();
                }

                PlaySpaceshipLevel(currentSpaceshipListIndex);
                gameState = GameState.playingLevel;
                break;
        }
    }

    public void SetCurrentRoom(SpaceshipRoom room)
    {
        previousRoom = currentRoom;
        currentRoom = room;
    }

    private void PlaySpaceshipLevel(int spaceshipListIndex)
    {
        bool spaceshipBuiltSuccessfully = SpaceshipBuilder.Instance.GenerateSpaceship(spaceshipLevelList[spaceshipListIndex]);

        if (!spaceshipBuiltSuccessfully)
        {
            Debug.Log("Couldn't build spaceship from specified rooms and node graphs");
        }

        StaticEventHandler.CallRoomChangedEvent(currentRoom);
        // GameObject.FindGameObjectWithTag("Player").transform.position =
        // new Vector3((currentRoom.lowerBounds.x + currentRoom.upperBounds.x)
        // / 2f, (currentRoom.lowerBounds.y + currentRoom.upperBounds.y) / 2f, 0f);

        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3((currentRoom.lowerBounds.x + currentRoom.upperBounds.x) / 2f, (currentRoom.lowerBounds.y + currentRoom.upperBounds.y) / 2f, 0f);

        // GameObject.FindGameObjectWithTag("Player").transform.position = HelperUtilities.GetSpawnPositionNearestToPlayer(GameObject.FindGameObjectWithTag("Player").gameObject.transform.position);

    }

    public SpaceshipLevelSO GetCurrentSpaceshipLevel()
    {
        return spaceshipLevelList[currentSpaceshipListIndex];
    }

    public SpaceshipRoom GetCurrentRoom()
    {
        return currentRoom;
    }


#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(spaceshipLevelList), spaceshipLevelList);
    }

#endif
}
