using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorEnemyAnimator : EnemyAnimator
{
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetIsProtectingTrue()
    {
        try
        {
            anim.SetBool("IsProtecting", true);
        }
        catch { }
    }

    public void SetIsProtectingFalse()
    {
        try
        {
            anim.SetBool("IsProtecting", false);
        }
        catch { }
    }
}
