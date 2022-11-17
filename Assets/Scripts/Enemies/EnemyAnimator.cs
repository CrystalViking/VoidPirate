using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour, IEnemyAnimator
{
    protected Animator anim;

    //private void Start()
    //{
    //    anim = GetComponent<Animator>();
    //}

    public void SetIsAttackingTrue()
    {
        anim.SetBool("IsAttacking", true);
    }

    public void SetIsAttackingFalse()
    {
        anim.SetBool("IsAttacking", false);
    }

    public void SetIsMovingTrue()
    {
        anim.SetBool("IsMoving", true);
    }

    public void SetIsMovingFalse()
    {
        anim.SetBool("IsMoving", false);
    }

    public void SetIsDeadTrue()
    {
        anim.SetBool("IsDead", true);
    }

    public void SetIsDeadFalse()
    {
        anim.SetBool("IsDead", false);
    }

}
