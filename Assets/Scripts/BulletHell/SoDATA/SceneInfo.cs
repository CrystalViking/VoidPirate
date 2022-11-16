using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneInfo : ScriptableObject
{
    [SerializeField]
    public bool isEventOn = false;

    private void OnEnable()
    {
        isEventOn = false;
    }
}
