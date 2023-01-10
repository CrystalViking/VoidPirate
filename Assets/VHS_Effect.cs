using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch.Runtime.AnalogGlitch;

public class VHS_Effect : MonoBehaviour
{
    private float health;
    public AnalogGlitchVolume analogGlitch;
    public bool isOnBulletHell;
    [SerializeField] Volume volume;
    [SerializeField] float scanLineJiterLowValue;
    [SerializeField] float verticalJumpLowValue;
    [SerializeField] float scanLineJiterHighValue;
    [SerializeField] float verticalJumpHighValue;
    void Start()
    {
        if (!isOnBulletHell)
            health = GetComponent<PlayerStats>().GetHealth();
        else
            health = GetComponent<PShipHealth>().health;
        volume.profile.TryGet<AnalogGlitchVolume>(out analogGlitch);
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
            analogGlitch.scanLineJitter.value = scanLineJiterLowValue;
            analogGlitch.verticalJump.value = verticalJumpLowValue;
        }
        else if (health < 40)
        {
            analogGlitch.scanLineJitter.value = scanLineJiterHighValue;
            analogGlitch.verticalJump.value = verticalJumpHighValue;
        }
        else
        {
            try
            {
                analogGlitch.scanLineJitter.value = 0.0f;
                analogGlitch.verticalJump.value = 0.0f;
            }
            catch { }
        }
    }
}
