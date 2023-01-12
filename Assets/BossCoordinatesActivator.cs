using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoordinatesActivator : MonoBehaviour
{
    public bool coordinatesScanned = false;
    public GameObject bossGame;
    public GameObject terminal;
    public BossLocationLoad bossLocationLoad;

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
            if (!bossLocationLoad.coordinatesLoaded)
            {
                TargetIndicator.instance.MarkTarget(bossGame.transform);
            }
            if (bossLocationLoad.coordinatesLoaded)
            {
                TargetIndicator.instance.MarkTarget(terminal.transform);
            }
        }
        
    }

    public void SetCoordinatesScanned()
    {
        coordinatesScanned = true;
    }

    public void UnsetCoordinatesScanned()
    {
        //TargetIndicator.instance.MarkTarget()
        coordinatesScanned = false;
    }
}
