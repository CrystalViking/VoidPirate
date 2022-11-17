using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAnimator : EnemyAnimator
{
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
}
