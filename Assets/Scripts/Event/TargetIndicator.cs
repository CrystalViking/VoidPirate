using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public static TargetIndicator instance;

    private bool lookingLeft;
    private bool lookingRight;

    private PlayerMovement playerMovement;
    public float HideDistance;

    void Start()
    {
        instance = this;
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        FetchLookingDirections();
        CorrectTransform();
    }

    private void FetchLookingDirections()
    {
        lookingLeft = playerMovement.IsLookingLeft();
        lookingRight = playerMovement.IsLookingRight();
    }

    private void CorrectTransform()
    {
        if(lookingLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(lookingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void MarkTarget(Transform target)
    {
        var dir = target.position - transform.position;

        if(dir.magnitude < HideDistance)
        {
            SetChildren(false);
        }

        else
        {
            SetChildren(true);
        }

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void SetChildren(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
    public void DeactivateIndicator()
    {
        SetChildren(false);
    }
}
