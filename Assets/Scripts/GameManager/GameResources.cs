using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }

    [Space(10)]
    [Header("Spaceship")]
    [Tooltip("Populate with the spaceship RoomNodeTypeListSO")]
    public RoomNodeTypeListSO roomNodeTypeList;

    // For shaders if we would like to add them in future
    [Space(10)]
    [Header("Materials")]
    [Tooltip("Dimmed Material")]
    public Material dimmedMaterial;
    public Material litMaterial;
    public Shader variableLitShader;
}
