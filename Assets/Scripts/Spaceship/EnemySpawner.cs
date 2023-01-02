using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemySpawner : SingletonMonobehaviour<EnemySpawner>
{
  private int enemiesToSpawn;
  private int currentEnemyCount;
  private int enemiesSpawnedSoFar;
  private int enemyMaxConcurrentSpawnNumber;
  private SpaceshipRoom currentRoom;
  private RoomEnemySpawnParameters roomEnemySpawnParameters;

  private void OnEnable()
  {
    // subscribe to room changed event
    StaticEventHandler.OnRoomChanged += StaticEventHandler_OnRoomChanged;
  }

  private void OnDisable()
  {
    // unsubscribe from room changed event
    StaticEventHandler.OnRoomChanged -= StaticEventHandler_OnRoomChanged;
  }

  /// <summary>
  /// Process a change in room
  /// </summary>
  private void StaticEventHandler_OnRoomChanged(RoomChangedEventArgs roomChangedEventArgs)
  {
    enemiesSpawnedSoFar = 0;
    currentEnemyCount = 0;

    currentRoom = roomChangedEventArgs.room;


    // if the room is a corridor or the entrance then return
    if (currentRoom.roomNodeType.isCorridorEW || currentRoom.roomNodeType.isCorridorNS || currentRoom.roomNodeType.isEntrance)
      return;

    // if the room has already been defeated then return
    if (currentRoom.isClearedOfEnemies) return;

    // Get random number of enemies to spawn
    enemiesToSpawn = currentRoom.GetNumberOfEnemiesToSpawn(SpaceshipGameManager.Instance.GetCurrentSpaceshipLevel());

    // Get room enemy spawn parameters
    roomEnemySpawnParameters = currentRoom.GetRoomEnemySpawnParameters(SpaceshipGameManager.Instance.GetCurrentSpaceshipLevel());

    // If no enemies to spawn return
    if (enemiesToSpawn == 0)
    {
      // Mark the room as cleared
      currentRoom.isClearedOfEnemies = true;

      return;
    }

    // Get concurrent number of enemies to spawn
    enemyMaxConcurrentSpawnNumber = GetConcurrentEnemies();

    // Lock doors
    currentRoom.instantiatedRoom.LockDoors();

    // Spawn enemies
    SpawnEnemies();
  }

  /// <summary>
  /// Spawn the enemies
  /// </summary>
  private void SpawnEnemies()
  {
    // Set gamestate engaging enemies
    if (SpaceshipGameManager.Instance.gameState == GameState.playingLevel)
    {
      SpaceshipGameManager.Instance.previousGameState = GameState.playingLevel;
      SpaceshipGameManager.Instance.gameState = GameState.engagingEnemies;
    }

    StartCoroutine(SpawnEnemiesRoutine());
  }

  /// <summary>
  /// Spawn the enemies coroutine
  /// </summary>
  private IEnumerator SpawnEnemiesRoutine()
  {
    Grid grid = currentRoom.instantiatedRoom.grid;

    // Create an instance of the helper class used to select a random enemy
    RandomSpawnableObject<EnemyDetailsSO> randomEnemyHelperClass = new RandomSpawnableObject<EnemyDetailsSO>(currentRoom.enemiesByLevelList);

    // Check we have somewhere to spawn the enemies
    if (currentRoom.spawnPositionArray.Length > 0)
    {
      // Loop through to create all the enemeies
      for (int i = 0; i < enemiesToSpawn; i++)
      {
        // wait until current enemy count is less than max concurrent enemies
        while (currentEnemyCount >= enemyMaxConcurrentSpawnNumber)
        {
          yield return null;
        }

        Vector3 enemySpawnPosition = currentRoom.instantiatedRoom.boxCollider2D.bounds.center;
        // Create Enemy - Get next enemy type to spawn 
        CreateEnemy(randomEnemyHelperClass.GetItem(), enemySpawnPosition);

        yield return new WaitForSeconds(GetEnemySpawnInterval());
      }
    }
  }

  /// <summary>
  /// Get a random spawn interval between the minimum and maximum values
  /// </summary>
  private float GetEnemySpawnInterval()
  {
    return (Random.Range(roomEnemySpawnParameters.minSpawnInterval, roomEnemySpawnParameters.maxSpawnInterval));
  }

  /// <summary>
  /// Get a random number of concurrent enemies between the minimum and maximum values
  /// </summary>
  private int GetConcurrentEnemies()
  {
    return (Random.Range(roomEnemySpawnParameters.minConcurrentEnemies, roomEnemySpawnParameters.maxConcurrentEnemies));
  }

  /// <summary>
  /// Create an enemy in the specified position
  /// </summary>
  private void CreateEnemy(EnemyDetailsSO enemyDetails, Vector3 position)
  {
    // keep track of the number of enemies spawned so far 
    enemiesSpawnedSoFar++;

    // Add one to the current enemy count - this is reduced when an enemy is destroyed
    currentEnemyCount++;

    // Get current dungeon level
    SpaceshipLevelSO dungeonLevel = SpaceshipGameManager.Instance.GetCurrentSpaceshipLevel();

    // Instantiate enemy
    GameObject enemy = Instantiate(enemyDetails.enemyPrefab, position, Quaternion.identity, transform);

    // Initialize Enemy - can be used to modify enemy behaviour
    enemy.GetComponent<Enemy>().EnemyInitialization();

    enemy.GetComponent<DestroyedEvent>().OnDestroyed += Enemy_OnDestroyed;
  }

  private void Enemy_OnDestroyed(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
  {
    // Unsubscribe from event
    destroyedEvent.OnDestroyed -= Enemy_OnDestroyed;

    // reduce current enemy count
    currentEnemyCount--;

    // Score points - call points scored event
    StaticEventHandler.CallPointsScoredEvent(destroyedEventArgs.points);

    if (currentEnemyCount <= 0 && enemiesSpawnedSoFar == enemiesToSpawn)
    {
      currentRoom.isClearedOfEnemies = true;

      // Set game state
      if (SpaceshipGameManager.Instance.gameState == GameState.engagingEnemies)
      {
        SpaceshipGameManager.Instance.gameState = GameState.playingLevel;
        SpaceshipGameManager.Instance.previousGameState = GameState.engagingEnemies;
      }

      else if (SpaceshipGameManager.Instance.gameState == GameState.engagingBoss)
      {
        SpaceshipGameManager.Instance.gameState = GameState.bossStage;
        SpaceshipGameManager.Instance.previousGameState = GameState.engagingBoss;
      }

      // unlock doors
      currentRoom.instantiatedRoom.UnlockDoors(0.1f);

      // Trigger room enemies defeated event
      StaticEventHandler.CallRoomEnemiesDefeatedEvent(currentRoom);
    }
  }

}