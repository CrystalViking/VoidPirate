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

  private void PlaySpaceshipLevel(int spaceshipListIndex)
  {
    throw new NotImplementedException();
  }

#if UNITY_EDITOR

  private void OnValidate()
  {
    HelperUtilities.ValidateCheckEnumerableValues(this, nameof(spaceshipLevelList), spaceshipLevelList);
  }

#endif
}
