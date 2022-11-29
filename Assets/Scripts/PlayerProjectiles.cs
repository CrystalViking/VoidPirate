using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectiles : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    private float lastShootTime = 0;
    public float fireRate;
    public AudioSource audioSource;

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
        
            if (Time.time > lastShootTime + fireRate)
            {
                lastShootTime = Time.time;
                GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
                audioSource.Play();
                Vector3 worldMousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos = new Vector2(worldMousePosition3D.x, worldMousePosition3D.y);
                Vector2 myPos = transform.position;
                Vector2 direction = (mousePos - myPos).normalized;
                spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
                spell.GetComponent<PlayerShipProjectile>().damage = Random.Range(minDamage, maxDamage);
                spell.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(mousePos - myPos));
            }
        }
    }
}
