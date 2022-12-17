using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParent : MonoBehaviour
{
    public Vector2 PointerPosition { get; set; }

    private void Update()
    {
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {

            scale.y = -1;
            scale.x = -1;

        }
        else if (direction.x > 0)
        {

            scale.y = 1;
            scale.x = 1;

        }
        transform.localScale = scale;
    }
}
