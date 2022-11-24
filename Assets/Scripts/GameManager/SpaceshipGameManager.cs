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
  private SpaceshipRoom currentRoom;
  private SpaceshipRoom previousRoom;

  void Start()
  {
    gameState = GameState.gameStarted;
  }

  void Update()
  {
    HandleGameState();

    if (Input.GetKeyDown(KeyCode.F8))
    {
      gameState = GameState.gameStarted;
    }
  }

  private void HandleGameState()
  {
    switch (gameState)
    {
      case GameState.gameStarted:
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
      Debug.Log("Couldn't build dungeon from specified rooms and node graphs");
    }

    GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3((currentRoom.lowerBounds.x + currentRoom.upperBounds.x) / 2f, (currentRoom.lowerBounds.y + currentRoom.upperBounds.y) / 2f, 0f);
  }

  public SpaceshipLevelSO GetCurrentSpaceshipLevel()
  {
    return spaceshipLevelList[currentSpaceshipListIndex];
  }

#if UNITY_EDITOR

  private void OnValidate()
  {
    HelperUtilities.ValidateCheckEnumerableValues(this, nameof(spaceshipLevelList), spaceshipLevelList);
  }

#endif
}
