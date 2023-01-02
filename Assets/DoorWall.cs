using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWall : MonoBehaviour
{
    public void Close()
    {
        foreach (BoxCollider2D boxCollider in GetComponentsInChildren<BoxCollider2D>())
        {
            boxCollider.enabled = false;
        }
    }

    public void Open()
    {
        foreach (BoxCollider2D boxCollider in GetComponentsInChildren<BoxCollider2D>())
        {
            boxCollider.enabled = true;
        }
    }
}
