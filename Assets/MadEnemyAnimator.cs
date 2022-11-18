using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadEnemyAnimator : EnemyAnimator
{
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetIsMovingOrAttackingTrue()
    {
        try
        {
            anim.SetBool("IsMovingOrAttacking", true);
        }
        catch { }
    }

    public void SetIsMovingOrAttackingFalse()
    {
        try
        {
            anim.SetBool("IsMovingOrAttacking", false);
        }
        catch { }
    }

}
