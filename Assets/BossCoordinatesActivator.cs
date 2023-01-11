using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoordinatesActivator : MonoBehaviour
{
    public bool coordinatesScanned = false;
    public GameObject bossGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coordinatesScanned)
        {
            bossGame.SetActive(true);
            TargetIndicator.instance.MarkTarget(bossGame.transform);
        }
    }
}
