using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoldEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject anyObject;
    private float startTime = 0f;
    private float timer = 0f;
    public float holdTime = 5.0f;
    private bool held = false;
    public string key = "e";
    private bool inRange = false;
    public Slider slider;
    public TextMeshProUGUI text;
    public GameObject chargeBar;
    public GameObject helpText;
    [SerializeField]
    private SceneInfo sceneInfo;
    public Animator alarm;
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
    private void Start()
    {
        if (sceneInfo.isEventOn == true)
        {
            audioSource.Play();
            alarm.SetBool("isAlarmOn", true);
        }
    }

    void Update()
    {
        if (sceneInfo.isEventOn == true)
        {
            if(held == false)
            {
                TargetIndicator.instance.MarkTarget(GameObject.Find("ShipMonitor").transform);
            }
            if (held)
            {
                TargetIndicator.instance.MarkTarget(anyObject.transform);
            }
         
            helpText.SetActive(true);

            if (inRange)
            {
                CheckIfButtonHeld();
            }
        }
    }
    void CheckIfButtonHeld()
    {
        if (Input.GetKeyDown(key))
        {
            startTime = Time.deltaTime;
            timer = startTime;
        }

        if (Input.GetKey(key) & held == false)
        {
            chargeBar.SetActive(true);
            timer += Time.deltaTime;
            SetSlider();
            if (timer > (startTime + holdTime))
            {
                held = true;
                ButtonHeld();
            }
        }
    }
    void ButtonHeld()
    {
        alarm.SetBool("isAlarmOn", false);
        audioSource.Stop();
        anyObject.SetActive(true);
    }

    public void SetSlider()
    {
        slider.value = timer;
    }
}
