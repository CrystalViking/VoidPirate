using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]
public class InstantiatedRoom : MonoBehaviour
{
  [HideInInspector] public SpaceshipRoom room;
  [HideInInspector] public Grid grid;
  [HideInInspector] public Tilemap groundTilemap;
  [HideInInspector] public Tilemap wallsTilemap;
  [HideInInspector] public Tilemap decoration1Tilemap;
  [HideInInspector] public Tilemap decoration2Tilemap;
  [HideInInspector] public Tilemap frontTilemap;
  //[HideInInspector]
  public Tilemap collisionTilemap;
  [HideInInspector] public Tilemap minimapTilemap;
  [HideInInspector] public Bounds roomColliderBounds;
  private BoxCollider2D boxCollider2D;

  private void Awake()
  {
    boxCollider2D = GetComponent<BoxCollider2D>();
    roomColliderBounds = boxCollider2D.bounds;
  }

  public void Initialise(GameObject roomGameObject)
  {
    PopulateTilemapMemberVariables(roomGameObject);
    BlockOffUnusedDoorways();
    DisableCollisionTilemapRenderer();
  }

  private void PopulateTilemapMemberVariables(GameObject roomGameObject)
  {
    grid = roomGameObject.GetComponentInChildren<Grid>();
    Tilemap[] tilemaps = roomGameObject.GetComponentsInChildren<Tilemap>();

    foreach (Tilemap tilemap in tilemaps)
    {
      if (tilemap.gameObject.tag == "GroundTilemap")
      {
        groundTilemap = tilemap;
      }
      if (tilemap.gameObject.tag == "WallsTilemap")
      {
        wallsTilemap = tilemap;
      }
      else if (tilemap.gameObject.tag == "Decoration1Tilemap")
      {
        decoration1Tilemap = tilemap;
      }
      else if (tilemap.gameObject.tag == "Decoration2Tilemap")
      {
        decoration2Tilemap = tilemap;
      }
      else if (tilemap.gameObject.tag == "FrontTilemap")
      {
        frontTilemap = tilemap;
      }
      else if (tilemap.gameObject.tag == "CollisionTilemap")
      {
        collisionTilemap = tilemap;
      }
      else if (tilemap.gameObject.tag == "MinimapTilemap")
      {
        minimapTilemap = tilemap;
      }
    }
  }

  private void DisableCollisionTilemapRenderer()
  {
    collisionTilemap.gameObject.GetComponent<TilemapRenderer>().enabled = false;
  }

  private void BlockOffUnusedDoorways()
  {
    foreach (Doorway doorway in room.doorwayList)
    {
      if (doorway.isConnected)
      {
        continue;
      }

      if (groundTilemap != null)
      {
        BlockADoorwayOnTilemapLayer(groundTilemap, doorway);
      }

      if (wallsTilemap != null)
      {
        BlockADoorwayOnTilemapLayer(wallsTilemap, doorway);
      }

      if (decoration1Tilemap != null)
      {
        BlockADoorwayOnTilemapLayer(decoration1Tilemap, doorway);
      }

      if (decoration2Tilemap != null)
      {
        BlockADoorwayOnTilemapLayer(decoration2Tilemap, doorway);
      }

      if (frontTilemap != null)
      {
        BlockADoorwayOnTilemapLayer(frontTilemap, doorway);
      }

      if (collisionTilemap != null)
      {
        BlockADoorwayOnTilemapLayer(collisionTilemap, doorway);
      }

      if (minimapTilemap != null)
      {
        BlockADoorwayOnTilemapLayer(minimapTilemap, doorway);
      }

    }
  }

  private void BlockADoorwayOnTilemapLayer(Tilemap tilemap, Doorway doorway)
  {
    switch (doorway.orientation)
    {
      case Orientation.north:
      case Orientation.south:
        BlockDoorwayHorizontally(tilemap, doorway);
        break;
      case Orientation.east:
      case Orientation.west:
        BlockDoorwayVertically(tilemap, doorway);
        break;
      case Orientation.none:
        break;
    }
  }

  private void BlockDoorwayVertically(Tilemap tilemap, Doorway doorway)
  {
    Vector2Int startPos = doorway.doorwayStartCopyPos;

    for (int yPos = 0; yPos < doorway.doorwayCopyTileHeight; yPos++)
    {
      for (int xPos = 0; xPos < doorway.doorwayCopyTileWidth; xPos++)
      {
        Matrix4x4 transformMatrix = tilemap.GetTransformMatrix(new Vector3Int(startPos.x + xPos, startPos.y - yPos, 0));
        tilemap.SetTile(new Vector3Int(startPos.x + xPos, startPos.y - 1 - yPos, 0),
          tilemap.GetTile(new Vector3Int(startPos.x + xPos, startPos.y - yPos, 0)));
        tilemap.SetTransformMatrix(new Vector3Int(startPos.x + 1 + xPos, startPos.y - yPos, 0), transformMatrix);
      }
    }
  }

  private void BlockDoorwayHorizontally(Tilemap tilemap, Doorway doorway)
  {
    Vector2Int startPos = doorway.doorwayStartCopyPos;

    for (int xPos = 0; xPos < doorway.doorwayCopyTileWidth; xPos++)
    {
      for (int yPos = 0; yPos < doorway.doorwayCopyTileHeight; yPos++)
      {
        Matrix4x4 transformMatrix = tilemap.GetTransformMatrix(new Vector3Int(startPos.x + xPos, startPos.y - yPos, 0));
        tilemap.SetTile(new Vector3Int(startPos.x + 1 + xPos, startPos.y - yPos, 0),
          tilemap.GetTile(new Vector3Int(startPos.x + xPos, startPos.y - yPos, 0)));
        tilemap.SetTransformMatrix(new Vector3Int(startPos.x + 1 + xPos, startPos.y - yPos, 0), transformMatrix);
      }
    }
  }
}
