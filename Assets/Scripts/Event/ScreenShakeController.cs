using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    public static ScreenShakeController instance;
    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;
    public float rotationMultiplier = 7.5f;

    public event EventHandler ShakeFinished;

    bool shakeStarted = false;
    bool shakeFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
  
    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0 )
        {
            shakeStarted = true;
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = UnityEngine.Random.Range(-1f, 1f) * shakePower;
            float yAmount = UnityEngine.Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }
        else if(shakeTimeRemaining <= 0 && shakeStarted)
        {
            shakeFinished = true;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * UnityEngine.Random.Range(-1f, 1f));

        if (shakeFinished)
        {
            ShakeFinished?.Invoke(this, EventArgs.Empty);
        }

        
    }

    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;

        shakeRotation = power * rotationMultiplier;

    }
}
