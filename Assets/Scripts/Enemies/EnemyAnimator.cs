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
        try
        {
            anim.SetBool("IsAttacking", true);
        }
        catch { }
    }

    public void SetIsAttackingFalse()
    {
        try
        {
            anim.SetBool("IsAttacking", false);
        }
        catch{ }
    }

    public void SetIsMovingTrue()
    {
        try
        {
            anim.SetBool("IsMoving", true);
        }
        catch { }
    }

    public void SetIsMovingFalse()
    {
        try
        {
            anim.SetBool("IsMoving", false);
        }
        catch { }
    }

    public void SetIsDeadTrue()
    {
        try
        {
            anim.SetBool("IsDead", true);
        }
        catch { }
    }

    public void SetIsDeadFalse()
    {
        try
        {
            anim.SetBool("IsDead", false);
        }
        catch { }
    }

}
