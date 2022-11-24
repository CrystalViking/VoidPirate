using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SpaceshipDoor : MonoBehaviour
{
  [Space(10)]
  [Header("Object references")]
  [Tooltip("Populate this with the BoxCollider2D component from the DoorCollider gameObject")]
  [SerializeField] private BoxCollider2D doorCollider;

  [HideInInspector] public bool isBossRoomDoor = false;
  private BoxCollider2D doorTrigger;
  private bool isOpen = false;
  private bool previouslyOpened = false;
  private Animator animator;

  private void Awake()
  {
    doorCollider.enabled = false;
    animator = GetComponent<Animator>();
    doorTrigger = GetComponent<BoxCollider2D>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    // Later we can add check for player weapon collision here if it has one
    if (collision.tag == "Player")
    {
      OpenDoor();
    }
  }

  private void OnEnable()
  {

  }

  public void OpenDoor()
  {
    if (!isOpen)
    {
      isOpen = true;
      previouslyOpened = true;
      doorCollider.enabled = false;
      doorTrigger.enabled = false;
    }
  }

  public void LockDoor()
  {
    isOpen = false;
    doorCollider.enabled = true;
    doorTrigger.enabled = false;
  }

  public void UnlockDoor()
  {
    doorCollider.enabled = false;
    doorTrigger.enabled = true;

    if (previouslyOpened == true)
    {
      isOpen = false;
      OpenDoor();
    }
  }

#if UNITY_EDITOR
  private void OnValidate()
  {
    HelperUtilities.ValidateCheckNullValue(this, nameof(doorCollider), doorCollider);
  }
#endif
}