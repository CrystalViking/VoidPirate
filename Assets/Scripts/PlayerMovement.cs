using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private Animator animator;
    private PlayerStats playerStats;
    private float scale;

    private Vector2 pointerInput;

    private WeaponParent weaponParent;


    [SerializeField]
    public bool weaponParent_on = false;

    private bool LOOKING_LEFT;
    private bool LOOKING_RIGHT;

    

    private void Start()
    {
        scale = 3;
        GetReferences();
        InitVariables();
    }
    private void Update()
    {
        SetMousePointerFromInput();
        if(weaponParent_on)
            UpdateWeaponParent();
        GetSpeed();
        TakeInput();
        Move();
        
    }

    private void UpdateWeaponParent()
    {
        weaponParent.PointerPosition = pointerInput;
    }

    private void Awake()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
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
            animator.SetLayerWeight(1, 0);
        }
    }

    private void TakeInput()
    {
        direction = Vector2.zero;

        var playerScreenPoint = Camera.main.WorldToScreenPoint(GameObject.Find("Player").transform.position);
        var mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

        if(mouse.x < playerScreenPoint.x)
        {
            transform.localScale = new Vector3(-scale, scale, scale);
            LOOKING_LEFT = true;
            LOOKING_RIGHT = false;
        }
        else
        {
            transform.localScale = new Vector3(scale, scale, scale);
            LOOKING_LEFT = false;
            LOOKING_RIGHT = true;
        }


        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            if(!LOOKING_RIGHT)
                transform.localScale = new Vector3(-scale, scale, scale);
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            if(!LOOKING_LEFT)
                transform.localScale = new Vector3(scale, scale, scale);
        }

    }


   

    private Vector2 GetPointerInput()
    {
        //pointerInput = Input.mousePosition;
        //weaponParent.PointerPosition = pointerInput;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void SetMousePointerFromInput()
    {
        pointerInput = GetPointerInput();
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }


    private void GetReferences()
    {
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void InitVariables()
    {
        speed = playerStats.GetSpeed();
        LOOKING_LEFT = false;
        LOOKING_RIGHT = true;
    }

    public void SetSpeed(float speedX)
    {
        speed = speedX;
    }

    public float GetSpeed()
    {
        speed = playerStats.GetSpeed();
        return speed;
    }
}
