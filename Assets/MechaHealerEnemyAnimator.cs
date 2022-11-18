using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaHealerEnemyAnimator : EnemyAnimator
{
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetIsHealingTrue()
    {
        try
        {
            anim.SetBool("IsHealing", true);
        }
        catch { }
    }

    public void SetIsHealingFalse()
    {
        try
        {
            anim.SetBool("IsHealing", false);
        }
        catch { }
    }
}
