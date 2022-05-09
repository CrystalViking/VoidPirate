using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;
    public int Height;
    public int X;
    public int Y;

    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene.");
            return;
        }
        Debug.Log(RoomController.instance);
        RoomController.instance.RegisterRoom(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.Log(transform.position);
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
