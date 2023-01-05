using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossLocationLoad : MonoBehaviour
{
    bool isInRange;
    bool isActive = true;
    private float startTime = 0f;
    private float timer = 0f;
    public float endTime = 1f;
    public Slider slider;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;
    private bool sliderActive = true;
    private bool sliderActive2 = false;
    private bool sliderActive3 = false;
    private bool sliderActive4 = false;
    private bool timerUpIsActive = true;
    private bool timerDownIsActive = false;
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
            if (terminalMenu.activeSelf == true)
            {
                SliderManager();
            }
        }
    }
    void SliderManager()
    {
        if(sliderActive)
        {
            if (timerUpIsActive)
            {
                startTime += Time.deltaTime;
                timer = startTime;
                SetSlider(slider, timer);
                if (timer > 1f)
                {
                    endTime = 1f;
                    timerUpIsActive = false;
                    timerDownIsActive = true;

                }
            }
            if (timerDownIsActive)
            {
                endTime -= Time.deltaTime;
                timer = endTime;
                SetSlider(slider, timer);
                if (timer < 0f)
                {
                    startTime = 0f;
                    timerUpIsActive = true;
                    timerDownIsActive = false;

                }
            }
            if (timer > 0.518 && timer < 0.554 && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("GG");
                sliderActive = false;
                sliderActive2 = true;
            }

        }
        if (sliderActive2)
        {
            if (timerUpIsActive)
            {
                startTime += Time.deltaTime;
                timer = startTime;
                SetSlider(slider2, timer);
                if (timer > 1f)
                {
                    endTime = 1f;
                    timerUpIsActive = false;
                    timerDownIsActive = true;

                }
            }
            if (timerDownIsActive)
            {
                endTime -= Time.deltaTime;
                timer = endTime;
                SetSlider(slider2, timer);
                if (timer < 0f)
                {
                    startTime = 0f;
                    timerUpIsActive = true;
                    timerDownIsActive = false;

                }
            }
            if (timer > 0.153 && timer < 0.190 && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("GG2");
                sliderActive2 = false;
                sliderActive3 = true;
            }

        }
        if (sliderActive3)
        {
            if (timerUpIsActive)
            {
                startTime += Time.deltaTime;
                timer = startTime;
                SetSlider(slider3, timer);
                if (timer > 1f)
                {
                    endTime = 1f;
                    timerUpIsActive = false;
                    timerDownIsActive = true;

                }
            }
            if (timerDownIsActive)
            {
                endTime -= Time.deltaTime;
                timer = endTime;
                SetSlider(slider3, timer);
                if (timer < 0f)
                {
                    startTime = 0f;
                    timerUpIsActive = true;
                    timerDownIsActive = false;

                }
            }
            if (timer > 0.771 && timer < 0.808 && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("GG3");
                sliderActive3 = false;
                sliderActive4 = true;
            }

        }
        if (sliderActive4)
        {
            if (timerUpIsActive)
            {
                startTime += Time.deltaTime;
                timer = startTime;
                SetSlider(slider4, timer);
                if (timer > 1f)
                {
                    endTime = 1f;
                    timerUpIsActive = false;
                    timerDownIsActive = true;

                }
            }
            if (timerDownIsActive)
            {
                endTime -= Time.deltaTime;
                timer = endTime;
                SetSlider(slider4, timer);
                if (timer < 0f)
                {
                    startTime = 0f;
                    timerUpIsActive = true;
                    timerDownIsActive = false;

                }
            }
            if (timer > 0.358 && timer < 0.397 && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("GG4");
                sliderActive4 = false;
                successText.gameObject.SetActive(true);
            }

        }

    }
    void StartSlider()
    {
        //timerIsActive = true;
    }
    void StopSlider()
    {
        //timerIsActive = false;
        //SetSlider(timer);
    }
    public void SetSlider(Slider slider, float timer)
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
