using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BaseProjectile
{
    private WeaponSO weapon;
    private float minDamage;
    private float maxDamage;
    public float delay { get; set; }

    BaseProjectile()
    {

    }

    BaseProjectile(WeaponSO weapon) : this()
    {
        minDamage = weapon.minDamage;
        maxDamage = weapon.maxDamage;
    }

    BaseProjectile(WeaponSO weapon, float delay = 1.0f) : this(weapon)
    {
        this.delay = delay;
    }

    /// <summary>
    /// Set stats for the projectile
    /// </summary>
    /// <param name="weapon"> Provide the Weapon class</param>
    /// <param name="delay"> Set the delay before projectile is destroyed (1.0f by default) </param>
    public void SetStats(WeaponSO weapon, float delay = 1.0f)
    {
        minDamage = weapon.minDamage;
        maxDamage = weapon.maxDamage;
        this.delay = delay;
    }


    /// <summary>
    /// Get minimum damage of projectile
    /// </summary>
    /// <returns> Returns float value od minimum damage </returns>
    public float GetMinDamage()
    {
        return minDamage;
    }

    /// <summary>
    /// Get maximum damage of projectile
    /// </summary>
    /// <returns> Returns float value of maximum damage </returns>
    public float GetMaxDamage()
    {
        return maxDamage;
    }

    /// <summary>
    /// Get delay value (time before projectile gets destroyed)
    /// </summary>
    /// <returns> float value of delay </returns>
    public float GetDelay()
    {
        return delay;
    }
    



    
}
