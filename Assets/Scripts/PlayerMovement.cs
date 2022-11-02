using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private Animator animator;
    private PlayerStats playerStats;

    private void Start()
    {
        GetReferences();
        InitVariables();
    }
    private void Update()
    {
        TakeInput();
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (direction.x != 0 || direction.y != 0)
        {
            SetAnimatorMovement(direction);
        }
        else
        {
            //animator.SetLayerWeight(1, 0);
            animator.SetBool("IsMoving", false);
        }
    }

    private void TakeInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
    }
    private void SetAnimatorMovement(Vector2 direction)
    {
        //animator.SetLayerWeight(1, 1);
        animator.SetBool("IsMoving", true);
        //animator.SetFloat("xDir", direction.x);
        //animator.SetFloat("yDir", direction.y);
    }


    private void GetReferences()
    {
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void InitVariables()
    {
        speed = playerStats.GetSpeed();
    }

    public void SetSpeed(float speedX)
    {
        speed = speedX;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
