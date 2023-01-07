using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayGraphScan : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DelayScan());
    }
    IEnumerator DelayScan()
    {
        var graphToScan = AstarPath.active.data.gridGraph;
        yield return new WaitForSeconds(2f);
        AstarPath.active.Scan(graphToScan);
    }
  
}
