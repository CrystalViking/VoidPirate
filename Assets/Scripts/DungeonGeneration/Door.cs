using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public BoxCollider2D doorCollider;

    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;
    public DoorWall doorWall;
    public bool isDeleted = false;
    private GameObject player;
    private float widthOffset = 3.5f;
    private Animator animator;

    private bool playerCanEnter;

    public bool PlayerCanEnter
    {
        get
        {
            return playerCanEnter;
        }
        private set
        {
            playerCanEnter = value;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCanEnter = true;
        if (!isDeleted)
            doorWall.gameObject.SetActive(false);
    }

    public void OpenDoor()
    {
        playerCanEnter = true;
        animator.SetBool(Animator.StringToHash("open"), true);
    }

    public void CloseDoor()
    {
        playerCanEnter = false;
        animator.SetBool(Animator.StringToHash("open"), false);
    }

    private void OnEnable()
    {
        animator.SetBool(Animator.StringToHash("open"), false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && playerCanEnter)
        {

            OpenDoor();

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
