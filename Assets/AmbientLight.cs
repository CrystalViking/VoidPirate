using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLight : MonoBehaviour
{
    public float intensity = 0.1f;

    void Start()
    {
        RenderSettings.ambientIntensity = intensity;
    }
}