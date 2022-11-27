using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticEventHandler
{
  public static event Action<RoomChangedEventArgs> OnRoomChanged;

  public static void CallSpaceshipRoomChangedEvent(SpaceshipRoom room)
  {
    OnRoomChanged?.Invoke(new RoomChangedEventArgs() { room = room });
  }
}

public class RoomChangedEventArgs : EventArgs
{
  public SpaceshipRoom room;
}
