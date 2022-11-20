using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 PointerPosition { get; set; }

    public Animator animator;

    public float delay = 0.3f;
    private bool attackBlocked;
    public AudioSource hit;


    public bool IsAttacking { get; private set; }

    public Transform circleOrigin;
    public float radius;

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Update()
    {

        if (IsAttacking)
            return;
        Vector2 direction = (PointerPosition-(Vector2)transform.position).normalized;
        transform.right = direction;


        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            
            scale.y = -1;
            scale.x = -1;

        }
        else if (direction.x > 0)
        {
           
            scale.y = 1;
            scale.x = 1;

        }
        transform.localScale = scale;


        if (Input.GetKey(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if(attackBlocked)
        {
            return;
        }

        hit.Play();
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());

    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            //Debug.Log(collider.name);

            if(collider.CompareTag("Enemy") || collider.CompareTag("Boss"))
            {
                collider.GetComponent<IEnemy>().TakeDamage(50);
            }
        }
    }

}
