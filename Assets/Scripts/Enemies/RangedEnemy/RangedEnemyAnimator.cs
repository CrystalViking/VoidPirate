using UnityEngine;

public class RangedEnemyAnimator: EnemyAnimator
{
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

}