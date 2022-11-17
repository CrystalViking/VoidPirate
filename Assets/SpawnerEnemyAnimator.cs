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
        anim.SetBool("IsSpawning", true);
    }

    public void SetIsSpawningFalse()
    {
        anim.SetBool("IsSpawning", false);
    }
}
