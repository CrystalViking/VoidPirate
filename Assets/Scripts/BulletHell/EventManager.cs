using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private SceneInfo sceneInfo;
    public GameObject[] eventObjectsList;
    public GameObject[] defaultObjectsList;
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
            foreach (GameObject obj in defaultObjectsList)
            {
                obj.SetActive(false);
            }
            audioSource.Play();
            alarm.SetBool("isAlarmOn", true);
            index = Random.Range(0, eventObjectsList.Length);
            eventObjectsList[index].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneInfo.isEventOn == true)
        {
            if (eventOn == false)
                TargetIndicator.instance.MarkTarget(eventObjectsList[index].transform);
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
