using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossLocation : MonoBehaviour
{
    bool isInRange;
    bool isActive = false;
    private float startTime = 0f;
    private float timer = 0f;
    public float endTime = 10f;
    public Slider slider;
    private bool timerIsActive = true;
    public TMP_Text successText;
    private GameObject anyObject;

    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;

    [SerializeField] GameObject terminalMenu;

    void Start()
    {
        anyObject = FindObjectOfType<FindMe>(true).gameObject;
    }

    void Update()
    {
        InteractOnAction();
    }

    public void InteractOnAction()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(itemInteractionCode))
            {
                terminalMenu.SetActive(true);
            }
            if(terminalMenu.activeSelf == true)
            {
                SliderManager();
            }
        }
        if (anyObject != null && isActive)
        {
            MarkObject(anyObject);
        }
    }
    void SliderManager()
    {
        if (timerIsActive) 
        {
            startTime += Time.deltaTime;
            timer = startTime;
            SetSlider(timer);
            if (timer >  endTime)
            {
                timerIsActive = false;
                successText.gameObject.SetActive(true);
                //if (anyObject != null)
                //{
                    Debug.Log("yeah");
                    anyObject.SetActive(true);
                    isActive = true;
                //}
            }
        }
    }
    void StartSlider()
    {
        timerIsActive = true;
    }
    void StopSlider()
    {
        timerIsActive = false;
        SetSlider(timer);
    }
    public void SetSlider(float timer)
    {
        slider.value = timer;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInRange = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInRange = false;
    }
    public void MarkObject(GameObject anyObject)
    {
        TargetIndicator.instance.MarkTarget(anyObject.transform);
    }
}
