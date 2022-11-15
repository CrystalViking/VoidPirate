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
        anim.SetBool("IsMovingOrAttacking", true);
    }

    public void SetIsMovingOrAttackingFalse()
    {
        anim.SetBool("IsMovingOrAttacking", false);
    }

}
