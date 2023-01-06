using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private SceneInfo sceneInfo;
    public GameObject[] ObjectsList;
    private int index;
    private bool eventOn = false;
    public Animator alarm;
    public AudioSource audioSource;
    public GameObject anyObject;
    public static EventManager instance;
    


    // Start is called before the first frame update
    void Start()
    {
       instance = this;
        if (sceneInfo.isEventOn == true)
        {
            audioSource.Play();
            alarm.SetBool("isAlarmOn", true);
            index = Random.Range(0, ObjectsList.Length);
            ObjectsList[index].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneInfo.isEventOn == true)
        {
            if (eventOn == false)
                TargetIndicator.instance.MarkTarget(ObjectsList[index].transform);
            else if (eventOn == true)
                TargetIndicator.instance.MarkTarget(anyObject.transform);
        }
    }

    public void EventComplete()
    {
        alarm.SetBool("isAlarmOn", false);
        audioSource.Stop();
        anyObject.SetActive(true);
        eventOn = true;
        
    }
}
