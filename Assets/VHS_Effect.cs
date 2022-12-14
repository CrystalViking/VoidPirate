using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class VHS_Effect : MonoBehaviour
{
    private float health;
    public AnalogGlitch analogGlitch;
    public bool isOnBulletHell;
    void Start()
    {
        if (!isOnBulletHell)
            health = GetComponent<PlayerStats>().GetHealth();
        else
            health = GetComponent<PShipHealth>().health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnBulletHell)
            health = GetComponent<PlayerStats>().GetHealth();
        else
            health = GetComponent<PShipHealth>().health;
        if (health < 100 && health >= 40)
        {
            analogGlitch.enabled = true;
            analogGlitch.scanLineJitter = 0.2f;
            analogGlitch.verticalJump = 0.02f;
        }
        else if (health < 40)
        {
            analogGlitch.enabled = true;
            analogGlitch.scanLineJitter = 0.5f;
            analogGlitch.verticalJump = 0.05f;
        }
        else
        {
            analogGlitch.enabled = false;
            analogGlitch.scanLineJitter = 0.0f;
            analogGlitch.verticalJump = 0.0f;
        }
    }
}
