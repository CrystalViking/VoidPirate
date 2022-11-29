using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAlarmEvent : MonoBehaviour
{
    public Animator alarm;
    [SerializeField]
    private SceneInfo sceneInfo;
    // Start is called before the first frame update
    void Start()
    {
        if (sceneInfo.isEventOn == true)
        {
            alarm.SetTrigger("Alarm");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
