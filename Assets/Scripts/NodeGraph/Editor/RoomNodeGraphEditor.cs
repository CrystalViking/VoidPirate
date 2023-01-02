using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

public class RoomNodeGraphEditor : EditorWindow
{
  private GUIStyle roomNodeStyle;
  private GUIStyle roomNodeSelectedStyle;
  private static RoomNodeGraphSO currentRoomNodeGraph;
  private Vector2 graphOffset;
  private Vector2 graphDrag;
  private RoomNodeSO currentRoomNode = null;
  private RoomNodeTypeListSO roomNodeTypeList;
  private const float width = 160f;
  private const float height = 75f;
  private const int padding = 25;
  private const int border = 12;
  private const float lineWidth = 3f;
  private const float lineArrowSize = 6f;
  private const float gridLarge = 100f;
  private const float gridSmall = 25f;




  [MenuItem("Room Node Graph Editor", menuItem = "Window/Ship Editor/Room Node Graph Editor")]
  private static void OpenWindow()
  {
    GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");
  }

  private void OnEnable()
  {
    Selection.selectionChanged += InspectorSelectionChanged;

    roomNodeStyle = new GUIStyle();
    roomNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
    roomNodeStyle.normal.textColor = Color.white;
    roomNodeStyle.padding = new RectOffset(padding, padding, padding, padding);
    roomNodeStyle.border = new RectOffset(border, border, border, border);

    roomNodeSelectedStyle = new GUIStyle();
    roomNodeSelectedStyle.normal.background = EditorGUIUtility.Load("node1 on") as Texture2D;
    roomNodeSelectedStyle.normal.textColor = Color.white;
    roomNodeSelectedStyle.padding = new RectOffset(padding, padding, padding, padding);
    roomNodeSelectedStyle.border = new RectOffset(border, border, border, border);

    roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
  }

  private void OnDisable()
  {
    Selection.selectionChanged -= InspectorSelectionChanged;
  }

  private void InspectorSelectionChanged()
  {
    RoomNodeGraphSO roomNodeGraph = Selection.activeObject as RoomNodeGraphSO;

    if (roomNodeGraph != null)
    {
      currentRoomNodeGraph = roomNodeGraph;
      GUI.changed = true;
    }
  }

  [OnOpenAsset(0)]
  public static bool OnDoubleClickAsset(int instanceId, int line)
  {
    RoomNodeGraphSO roomNodeGraph = EditorUtility.InstanceIDToObject(instanceId) as RoomNodeGraphSO;

    if (roomNodeGraph != null)
    {
      OpenWindow();
      currentRoomNodeGraph = roomNodeGraph;
      return true;
    }
    return false;
  }

  private void OnGUI()
  {
    if (currentRoomNodeGraph != null)
    {
      DrawBackgroundGrid(gridSmall, 0.2f, Color.gray);
      DrawBackgroundGrid(gridLarge, 0.3f, Color.gray);
      DrawDraggedLine();
      ProcessEvents(Event.current);
      DrawRoomNodeConnections();
      DrawRoomNodes();
    }


    if (GUI.changed)
    {
      Repaint();
    }
  }

  private void DrawBackgroundGrid(float size, float opacity, Color color)
  {
    int verticalLineCount = Mathf.CeilToInt((position.width + size) / size);
    int horizontalLineCount = Mathf.CeilToInt((position.height + size) / size);
    Handles.color = new Color(color.r, color.g, color.b, opacity);
    graphOffset += graphDrag * 0.5f;
    Vector3 gridOffset = new Vector3(graphOffset.x % size, graphOffset.y % size, 0);

    for (int i = 0; i < verticalLineCount; i++)
    {
      Handles.DrawLine(new Vector3(size * i, -size, 0) + gridOffset, new Vector3(size * i, position.height + size, 0) + gridOffset);
    }

    for (int i = 0; i < horizontalLineCount; i++)
    {
      Handles.DrawLine(new Vector3(-size, size * i, 0) + gridOffset, new Vector3(position.width + size, size * i, 0) + gridOffset);
    }

    Handles.color = Color.white;
  }

  private void DrawRoomNodeConnections()
  {
    foreach (RoomNodeSO roomNode in currentRoomNodeGraph.roomNodeList)
    {
      if (roomNode.childRoomNodeIdList.Count > 0)
      {
        foreach (string childRoomNodeId in roomNode.childRoomNodeIdList)
        {
          if (currentRoomNodeGraph.roomNodeDictionary.ContainsKey(childRoomNodeId))
          {
            DrawConnectionLine(roomNode, currentRoomNodeGraph.roomNodeDictionary[childRoomNodeId]);

            GUI.changed = true;
          }
        }
      }
    }
  }

  private void DrawConnectionLine(RoomNodeSO parentRoomNode, RoomNodeSO childRoomNode)
  {
    Vector2 startPos = parentRoomNode.rect.center;
    Vector2 endPos = childRoomNode.rect.center;
    Vector2 midPos = (startPos + endPos) / 2f;
    Vector2 direction = endPos - startPos;

    // Arrow
    Vector2 arrowTailPoint1 = midPos - new Vector2(-direction.y, direction.x).normalized * lineArrowSize;
    Vector2 arrowTailPoint2 = midPos + new Vector2(-direction.y, direction.x).normalized * lineArrowSize;
    Vector2 arrowHeadPoint = midPos + direction.normalized * lineArrowSize;
    Handles.DrawBezier(arrowHeadPoint, arrowTailPoint1, arrowHeadPoint, arrowTailPoint1, Color.white, null, lineWidth);
    Handles.DrawBezier(arrowHeadPoint, arrowTailPoint2, arrowHeadPoint, arrowTailPoint2, Color.white, null, lineWidth);

    //Line
    Handles.DrawBezier(startPos, endPos, startPos, endPos, Color.white, null, lineWidth);

    GUI.changed = true;

  }

  private void DrawDraggedLine()
  {
    if (currentRoomNodeGraph.linePosition != Vector2.zero)
    {
      Handles.DrawBezier(currentRoomNodeGraph.roomNodeToDrawLineFrom.rect.center, currentRoomNodeGraph.linePosition,
      currentRoomNodeGraph.roomNodeToDrawLineFrom.rect.center, currentRoomNodeGraph.linePosition, Color.white, null, lineWidth);
    }
  }

  private void ProcessEvents(Event currentEvent)
  {
    graphDrag = Vector2.zero;

    if (currentRoomNode == null || currentRoomNode.isLeftClickDragging == false)
    {
      currentRoomNode = IsMouseOverRoomNode(currentEvent);
    }

    if (currentRoomNode == null || currentRoomNodeGraph.roomNodeToDrawLineFrom != null)
    {
      ProcessRoomNodeGraphEvents(currentEvent);
    }
    else
    {
      currentRoomNode.ProcessEvents(currentEvent);
    }
  }

  private RoomNodeSO IsMouseOverRoomNode(Event currentEvent)
  {
    for (int i = currentRoomNodeGraph.roomNodeList.Count - 1; i >= 0; i--)
    {
      if (currentRoomNodeGraph.roomNodeList[i].rect.Contains(currentEvent.mousePosition))
      {
        return currentRoomNodeGraph.roomNodeList[i];
      }
    }
    return null;
  }

  private void ProcessRoomNodeGraphEvents(Event currentEvent)
  {
    switch (currentEvent.type)
    {
      case EventType.MouseDown:
        ProcessMouseDownEvent(currentEvent);
        break;
      case EventType.MouseDrag:
        ProcessMouseDragEvent(currentEvent);
        break;
      case EventType.MouseUp:
        ProcessMouseUpEvent(currentEvent);
        break;
      default:
        break;
    }
  }

  private void ProcessMouseUpEvent(Event currentEvent)
  {
    if (currentEvent.button == 1 && currentRoomNodeGraph.roomNodeToDrawLineFrom != null)
    {
      RoomNodeSO roomNode = IsMouseOverRoomNode(currentEvent);

      if (roomNode != null)
      {
        if (currentRoomNodeGraph.roomNodeToDrawLineFrom.AddChildRoomNodeIdToRoomNode(roomNode.id))
        {
          roomNode.AddParentRoomNodeIdToRoomNode(currentRoomNodeGraph.roomNodeToDrawLineFrom.id);
        }
      }

      ClearLineDrag();
    }
  }

  private void ClearLineDrag()
  {
    currentRoomNodeGraph.roomNodeToDrawLineFrom = null;
    currentRoomNodeGraph.linePosition = Vector2.zero;
    GUI.changed = true;
  }

  private void ProcessMouseDragEvent(Event currentEvent)
  {
    if (currentEvent.button == 0)
    {
      ProcessLeftMouseDragEvent(currentEvent.delta);
    }


    if (currentEvent.button == 1)
    {
      ProcessRightMouseDragEvent(currentEvent);
    }
  }

  private void ProcessLeftMouseDragEvent(Vector2 dragDelta)
  {
    graphDrag = dragDelta;

    for (int i = 0; i < currentRoomNodeGraph.roomNodeList.Count; i++)
    {
      currentRoomNodeGraph.roomNodeList[i].DragNode(dragDelta);
    }
    GUI.changed = true;
  }

  private void ProcessRightMouseDragEvent(Event currentEvent)
  {
    if (currentRoomNodeGraph.roomNodeToDrawLineFrom != null)
    {
      DragConnectingLine(currentEvent.delta);
      GUI.changed = true;
    }
  }

  private void DragConnectingLine(Vector2 delta)
  {
    currentRoomNodeGraph.linePosition += delta;
  }

  private void ProcessMouseDownEvent(Event currentEvent)
  {
    if (currentEvent.button == 0)
    {
      ClearLineDrag();
      ClearAllSelectedRoomNodes();
    }

    if (currentEvent.button == 1)
    {
      ShowContextMenu(currentEvent.mousePosition);
    }
  }

  private void ClearAllSelectedRoomNodes()
  {
    foreach (RoomNodeSO roomNode in currentRoomNodeGraph.roomNodeList)
    {
      if (roomNode.isSelected)
      {
        roomNode.isSelected = false;
        GUI.changed = true;
      }
    }
  }

  private void ShowContextMenu(Vector2 mousePosition)
  {
    GenericMenu menu = new GenericMenu();
    menu.AddItem(new GUIContent("Create Room Node"), false, CreateRoomNode, mousePosition);
    menu.AddSeparator("");
    menu.AddItem(new GUIContent("Select All Room Nodes"), false, SelectAllRoomNodes);
    menu.AddSeparator("");
    menu.AddItem(new GUIContent("Delete Selected Room Node Links"), false, DeleteSelectedRoomNodeLinks);
    menu.AddItem(new GUIContent("Delete Selected Room Nodes"), false, DeleteSelectedRoomNodes);

    menu.ShowAsContext();
  }

  private void DeleteSelectedRoomNodes()
  {
    Queue<RoomNodeSO> roomNodesToDelete = new Queue<RoomNodeSO>();

    foreach (RoomNodeSO roomNode in currentRoomNodeGraph.roomNodeList)
    {
      if (roomNode.isSelected && !roomNode.roomNodeType.isEntrance)
      {
        roomNodesToDelete.Enqueue(roomNode);

        foreach (string childRoomNodeId in roomNode.childRoomNodeIdList)
        {
          RoomNodeSO childRoomNode = currentRoomNodeGraph.GetRoomNode(childRoomNodeId);

          if (childRoomNode != null)
          {
            childRoomNode.RemoveParentRoomNodeIdFromRoomNode(roomNode.id);
          }
        }

        foreach (string parentRoomNodeId in roomNode.parentRoomNodeIdList)
        {
          RoomNodeSO parentRoomNode = currentRoomNodeGraph.GetRoomNode(parentRoomNodeId);

          if (parentRoomNode != null)
          {
            parentRoomNode.RemoveChildRoomNodeIdFromRoomNode(roomNode.id);
          }
        }
      }
    }

    while (roomNodesToDelete.Count > 0)
    {
      RoomNodeSO roomNodeToDelete = roomNodesToDelete.Dequeue();
      currentRoomNodeGraph.roomNodeDictionary.Remove(roomNodeToDelete.id);
      currentRoomNodeGraph.roomNodeList.Remove(roomNodeToDelete);
      DestroyImmediate(roomNodeToDelete, true);
      AssetDatabase.SaveAssets();
    }
  }

  private void DeleteSelectedRoomNodeLinks()
  {
    foreach (RoomNodeSO roomNode in currentRoomNodeGraph.roomNodeList)
    {
      if (roomNode.isSelected && roomNode.childRoomNodeIdList.Count > 0)
      {
        for (int i = roomNode.childRoomNodeIdList.Count - 1; i >= 0; i--)
        {
          RoomNodeSO childRoomNode = currentRoomNodeGraph.GetRoomNode(roomNode.childRoomNodeIdList[i]);

          if (childRoomNode != null && childRoomNode.isSelected)
          {
            roomNode.RemoveChildRoomNodeIdFromRoomNode(childRoomNode.id);
            childRoomNode.RemoveParentRoomNodeIdFromRoomNode(roomNode.id);
          }
        }
      }
    }

    ClearAllSelectedRoomNodes();
  }

  private void SelectAllRoomNodes()
  {
    foreach (RoomNodeSO roomNode in currentRoomNodeGraph.roomNodeList)
    {
      roomNode.isSelected = true;
    }

    GUI.changed = true;
  }

  private void CreateRoomNode(object mousePositionObject)
  {
    if (currentRoomNodeGraph.roomNodeList.Count == 0)
    {
      CreateRoomNode(new Vector2(200f, 200f), roomNodeTypeList.list.Find(x => x.isEntrance));
    }

    CreateRoomNode(mousePositionObject, roomNodeTypeList.list.Find(x => x.isNone));
  }

  private void CreateRoomNode(object mousePositionObject, RoomNodeTypeSO roomNodeType)
  {
    Vector2 mousePosition = (Vector2)mousePositionObject;
    RoomNodeSO roomNode = ScriptableObject.CreateInstance<RoomNodeSO>();
    currentRoomNodeGraph.roomNodeList.Add(roomNode);
    roomNode.Initialise(new Rect(mousePosition, new Vector2(width, height)), currentRoomNodeGraph, roomNodeType);
    AssetDatabase.AddObjectToAsset(roomNode, currentRoomNodeGraph);
    AssetDatabase.SaveAssets();
    currentRoomNodeGraph.OnValidate();
  }

  private void DrawRoomNodes()
  {
    foreach (RoomNodeSO roomNode in currentRoomNodeGraph.roomNodeList)
    {
      if (roomNode.isSelected)
      {
        roomNode.Draw(roomNodeSelectedStyle);
      }
      else
      {
        roomNode.Draw(roomNodeStyle);
      }
    }

    GUI.changed = true;
  }
}
