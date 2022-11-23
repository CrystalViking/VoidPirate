using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform target;

    private bool lookingLeft;
    private bool lookingRight;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        MarkTarget(target);
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

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
