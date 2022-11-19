using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class RoomNodeSO : ScriptableObject
{
  [HideInInspector] public string id;
  [HideInInspector] public List<string> parentRoomNodeIdList = new List<string>();
  [HideInInspector] public List<string> childRoomNodeIdList = new List<string>();
  [HideInInspector] public RoomNodeGraphSO roomNodeGraph;
  public RoomNodeTypeSO roomNodeType;
  [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;


#if UNITY_EDITOR

  [HideInInspector] public Rect rect;
  [HideInInspector] public bool isLeftClickDragging = false;
  [HideInInspector] public bool isSelected = false;


  public void Initialise(Rect rect, RoomNodeGraphSO nodeGraph, RoomNodeTypeSO roomNodeType)
  {
    this.rect = rect;
    this.id = Guid.NewGuid().ToString();
    this.name = "RoomNode";
    this.roomNodeGraph = nodeGraph;
    this.roomNodeType = roomNodeType;
    roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
  }


  public void Draw(GUIStyle nodeStyle)
  {
    GUILayout.BeginArea(rect, nodeStyle);
    EditorGUI.BeginChangeCheck();

    if (parentRoomNodeIdList.Count > 0 || roomNodeType.isEntrance)
    {
      EditorGUILayout.LabelField(roomNodeType.roomNodeTypeName);
    }
    else
    {
      int selected = roomNodeTypeList.list.FindIndex(x => x == roomNodeType);
      int selection = EditorGUILayout.Popup("", selected, GetRoomNodeTypesToDisplay());
      roomNodeType = roomNodeTypeList.list[selection];

      if (roomNodeTypeList.list[selected].isCorridor && !roomNodeTypeList.list[selection].isCorridor ||
       !roomNodeTypeList.list[selected].isCorridor && roomNodeTypeList.list[selection].isCorridor)
      {
        if (childRoomNodeIdList.Count > 0)
        {
          for (int i = childRoomNodeIdList.Count - 1; i >= 0; i--)
          {
            RoomNodeSO childRoomNode = roomNodeGraph.GetRoomNode(childRoomNodeIdList[i]);

            if (childRoomNode != null)
            {
              RemoveChildRoomNodeIdFromRoomNode(childRoomNode.id);
              childRoomNode.RemoveParentRoomNodeIdFromRoomNode(id);
            }
          }
        }
      }
    }


    if (EditorGUI.EndChangeCheck())
    {
      EditorUtility.SetDirty(this);
    }

    GUILayout.EndArea();
  }

  private string[] GetRoomNodeTypesToDisplay()
  {
    string[] roomArray = new string[roomNodeTypeList.list.Count];

    for (int i = 0; i < roomNodeTypeList.list.Count; i++)
    {
      if (roomNodeTypeList.list[i].displayInNodeGraphEditor)
      {
        roomArray[i] = roomNodeTypeList.list[i].roomNodeTypeName;
      }
    }

    return roomArray;
  }

  public void ProcessEvents(Event currentEvent)
  {
    switch (currentEvent.type)
    {
      case EventType.MouseDown:
        ProcessMouseDownEvent(currentEvent);
        break;
      case EventType.MouseUp:
        ProcessMouseUpEvent(currentEvent);
        break;
      case EventType.MouseDrag:
        ProcessMouseDragEvent(currentEvent);
        break;
      default:
        break;
    }
  }

  private void ProcessMouseDownEvent(Event currentEvent)
  {
    if (currentEvent.button == 0)
    {
      ProcessLeftClickDownEvent();
    }
    else if (currentEvent.button == 1)
    {
      ProcessRightClickDownEvent(currentEvent);
    }
  }

  private void ProcessRightClickDownEvent(Event currentEvent)
  {
    roomNodeGraph.SetNodeToDrawConnectionLineFrom(this, currentEvent.mousePosition);
  }

  private void ProcessLeftClickDownEvent()
  {
    Selection.activeObject = this;

    isSelected = !isSelected;
  }

  private void ProcessMouseUpEvent(Event currentEvent)
  {
    if (currentEvent.button == 0)
    {
      ProcessLeftClickUpEvent();
    }
  }

  private void ProcessLeftClickUpEvent()
  {
    if (isLeftClickDragging)
    {
      isLeftClickDragging = false;
    }
  }

  private void ProcessMouseDragEvent(Event currentEvent)
  {
    if (currentEvent.button == 0)
    {
      ProcessLeftClickDragEvent(currentEvent);
    }
  }

  private void ProcessLeftClickDragEvent(Event currentEvent)
  {
    isLeftClickDragging = true;
    DragNode(currentEvent.delta);
    GUI.changed = true;
  }

  public void DragNode(Vector2 delta)
  {
    rect.position += delta;
    EditorUtility.SetDirty(this);
  }

  public bool AddChildRoomNodeIdToRoomNode(string childId)
  {
    if (IsChildRoomValid(childId))
    {
      childRoomNodeIdList.Add(childId);
      return true;
    }

    return false;
  }

  private bool IsChildRoomValid(string childId)
  {
    if (roomNodeGraph.GetRoomNode(childId).roomNodeType.isNone)
    {
      Debug.Log("");
      return false;
    }

    if (childRoomNodeIdList.Contains(childId))
    {
      Debug.Log("");
      return false;
    }

    if (id == childId)
    {
      Debug.Log("");
      return false;
    }

    if (parentRoomNodeIdList.Contains(childId))
    {
      Debug.Log("");
      return false;
    }

    if (roomNodeGraph.GetRoomNode(childId).parentRoomNodeIdList.Count > 0)
    {
      Debug.Log("");
      return false;
    }

    if (roomNodeGraph.GetRoomNode(childId).roomNodeType.isCorridor && roomNodeType.isCorridor)
    {
      Debug.Log("");
      return false;
    }

    if (!roomNodeGraph.GetRoomNode(childId).roomNodeType.isCorridor && !roomNodeType.isCorridor)
    {
      Debug.Log("");
      return false;
    }

    if (roomNodeGraph.GetRoomNode(childId).roomNodeType.isCorridor && childRoomNodeIdList.Count >= Settings.maxChildCorridors)
    {
      Debug.Log("");
      return false;
    }

    if (roomNodeGraph.GetRoomNode(childId).roomNodeType.isEntrance)
    {
      Debug.Log("");
      return false;
    }

    if (!roomNodeGraph.GetRoomNode(childId).roomNodeType.isCorridor && childRoomNodeIdList.Count > 0)
    {
      Debug.Log("");
      return false;
    }

    return true;
  }

  public bool AddParentRoomNodeIdToRoomNode(string parentId)
  {
    parentRoomNodeIdList.Add(parentId);
    return true;
  }

  public bool RemoveChildRoomNodeIdFromRoomNode(string childId)
  {
    if (childRoomNodeIdList.Contains(childId))
    {
      childRoomNodeIdList.Remove(childId);
      return true;
    }

    return false;
  }

  public bool RemoveParentRoomNodeIdFromRoomNode(string parentId)
  {
    if (parentRoomNodeIdList.Contains(parentId))
    {
      parentRoomNodeIdList.Remove(parentId);
      return true;
    }

    return false;
  }

#endif

}
