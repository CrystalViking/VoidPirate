using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;
    public GameObject doorCollider;
    private GameObject player;
    private float widthOffset = 3.5f;

    private bool playerCanEnter;

    public bool PlayerCanEnter {
        get
        {
            return playerCanEnter;
        }
        private set
        {
            playerCanEnter = value;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCanEnter = false;
    }


    public void OpenDoor()
    {
        playerCanEnter = true;
    }

    public void CloseDoor()
    {
        playerCanEnter = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && playerCanEnter)
        {
            switch (doorType)
            {
                case DoorType.bottom:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y - widthOffset);
                    break;
                case DoorType.left:
                    player.transform.position = new Vector2(transform.position.x - widthOffset, transform.position.y);
                    break;
                case DoorType.right:
                    player.transform.position = new Vector2(transform.position.x + widthOffset, transform.position.y);
                    break;
                case DoorType.top:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y + widthOffset);
                    break;
            }
        }
    }
}
