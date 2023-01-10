using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoom5 : MonoBehaviour
{
    public GameObject terminal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer.Instance.didEnergyEventSucceed == true && Timer.Instance.didOxygenEventSucceed)
        {
            terminal.SetActive(true);
        }
    }
}
