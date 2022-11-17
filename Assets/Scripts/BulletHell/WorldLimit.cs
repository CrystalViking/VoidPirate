using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLimit : MonoBehaviour
{
    public float HorizontalBorder = 19.26f;
    public float VerticalBorder = 24.96f;
    void Update()
    {
        if (transform.position.x < -HorizontalBorder)
            transform.position = new Vector3(-HorizontalBorder, transform.position.y, transform.position.z);
        if (transform.position.x > HorizontalBorder)
            transform.position = new Vector3(HorizontalBorder, transform.position.y, transform.position.z);
        if (transform.position.y < -VerticalBorder)
            transform.position = new Vector3(transform.position.x, -VerticalBorder, transform.position.z);
        if (transform.position.y > VerticalBorder)
            transform.position = new Vector3(transform.position.x, VerticalBorder, transform.position.z);
    }
}
