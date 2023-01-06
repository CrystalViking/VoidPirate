using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossScript : MonoBehaviour
{
    public GameObject cross;
    private Vector3 screenPos;
    private Vector3 worldPos;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        screenPos = Input.mousePosition;
        screenPos.z = Camera.main.nearClipPlane + 1;

        worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        cross.transform.position = worldPos;
    }
}
