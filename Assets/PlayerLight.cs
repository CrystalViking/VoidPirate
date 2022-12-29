using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public float intensity = 0.5f;

    void Start()
    {
        GameObject lightObject = new GameObject("PlayerLight");
        lightObject.transform.parent = gameObject.transform;
        lightObject.transform.localPosition = Vector3.zero;

        Light playerLight = lightObject.AddComponent<Light>();

        playerLight.intensity = intensity;
    }
}