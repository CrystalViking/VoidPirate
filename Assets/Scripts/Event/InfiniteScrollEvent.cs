using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollEvent : MonoBehaviour
{
    [SerializeField]
    private SceneInfo sceneInfo;
    Material material;
    Vector2 offset;


    public float xVelocity, yVelocity;


    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if(sceneInfo.isEventOn)
        {
            offset = new Vector2(xVelocity, yVelocity);
            material.mainTextureOffset += offset * Time.deltaTime;
        }
        
    }
}