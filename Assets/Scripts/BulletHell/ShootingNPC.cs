using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingNPC : MonoBehaviour
{
    public GameObject bullet;
    public float timeBetweenShots;
    private float nextShotTime;
    private Transform target;
    private Animator anim;
    public float minDamage;
    public float maxDamage;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Enemy").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time > nextShotTime)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextShotTime = Time.time + timeBetweenShots;
          
        }
    }
}

