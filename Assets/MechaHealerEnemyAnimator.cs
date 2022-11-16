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
        anim.SetBool("IsHealing", true);
    }

    public void SetIsHealingFalse()
    {
        anim.SetBool("IsHealing", false);
    }
}
