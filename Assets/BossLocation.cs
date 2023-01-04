using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossLocation : MonoBehaviour
{
    bool isInRange;
    bool isActive = true;
    private float startTime = 0f;
    private float timer = 0f;
    public float endTime = 10f;
    public Slider slider;
    private bool timerIsActive = true;
    public TMP_Text successText;

    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;

    [SerializeField] GameObject terminalMenu;

    void Start()
    {

    }

    void Update()
    {
        InteractOnAction();
    }

    public void InteractOnAction()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(itemInteractionCode) && isActive)
            {
                terminalMenu.SetActive(true);
            }
            if(terminalMenu.activeSelf == true)
            {
                SliderManager();
            }
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
}
