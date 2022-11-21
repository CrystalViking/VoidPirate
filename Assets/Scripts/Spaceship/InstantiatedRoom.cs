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
  [HideInInspector] public Tilemap decoration1Tilemap;
  [HideInInspector] public Tilemap decoration2Tilemap;
  [HideInInspector] public Tilemap frontTilemap;
  [HideInInspector] public Tilemap collisionTilemap;
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
}
