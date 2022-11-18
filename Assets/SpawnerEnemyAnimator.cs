using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemyAnimator : EnemyAnimator
{
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetIsSpawningTrue()
    {
        try
        {
            anim.SetBool("IsSpawning", true);
        }
        catch { }
    }

    public void SetIsSpawningFalse()
    {
        try
        {
            anim.SetBool("IsSpawning", false);
        }
        catch { }
    }
}
