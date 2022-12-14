using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticEventHandler
{
  public static event Action<RoomChangedEventArgs> OnRoomChanged;

  public static void CallRoomChangedEvent(SpaceshipRoom room)
  {
    OnRoomChanged?.Invoke(new RoomChangedEventArgs() { room = room });
  }

  public static event Action<RoomEnemiesDefeatedArgs> OnRoomEnemiesDefeated;

  public static void CallRoomEnemiesDefeatedEvent(SpaceshipRoom room)
  {
    OnRoomEnemiesDefeated?.Invoke(new RoomEnemiesDefeatedArgs() { room = room });
  }

  // Points scored event
  public static event Action<PointsScoredArgs> OnPointsScored;

  public static void CallPointsScoredEvent(int points)
  {
    OnPointsScored?.Invoke(new PointsScoredArgs() { points = points });
  }
}

public class RoomChangedEventArgs : EventArgs
{
  public SpaceshipRoom room;
}

public class RoomEnemiesDefeatedArgs : EventArgs
{
  public SpaceshipRoom room;
}

public class PointsScoredArgs : EventArgs
{
  public int points;
}

