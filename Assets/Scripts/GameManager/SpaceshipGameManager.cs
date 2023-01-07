using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public GameObject text0;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;

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

        if (currentSpaceshipListIndex == 1)
        {
            switch (room.roomNodeType.roomNodeTypeName)
            {
                case ("Entrance"):
                    text0.SetActive(true);
                    text1.SetActive(false);
                    text2.SetActive(false);
                    text3.SetActive(false);
                    text4.SetActive(false);
                    text5.SetActive(false);
                    break;
                case ("Tutorial room 1"):
                    text0.SetActive(false);
                    text1.SetActive(true);
                    text2.SetActive(false);
                    text3.SetActive(false);
                    text4.SetActive(false);
                    text5.SetActive(false);
                    break;
                case ("Tutorial room 2"):
                    text0.SetActive(false);
                    text1.SetActive(false);
                    text2.SetActive(true);
                    text3.SetActive(false);
                    text4.SetActive(false);
                    text5.SetActive(false);
                    break;
                case ("Tutorial room 3"):
                    text0.SetActive(false);
                    text1.SetActive(false);
                    text2.SetActive(false);
                    text3.SetActive(true);
                    text4.SetActive(false);
                    text5.SetActive(false);
                    break;
                case ("Tutorial room 4"):
                    text0.SetActive(false);
                    text1.SetActive(false);
                    text2.SetActive(false);
                    text3.SetActive(false);
                    text4.SetActive(true);
                    text5.SetActive(false);
                    break;
                case ("Tutorial room 5"):
                    text0.SetActive(false);
                    text1.SetActive(false);
                    text2.SetActive(false);
                    text3.SetActive(false);
                    text4.SetActive(false);
                    text5.SetActive(true);
                    break;
            }
        }

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
