using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public GameObject projectile;
    private Weapon currentWeapon;

    private float lastShootTime = 0;


    private PlayerInventory inventory;
    private EquipmentManager manager;

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }


    private void Start()
    {
        GetReferences();
        currentWeapon = inventory.GetItem(manager.CurrentlyEquippedWeapon());

    }


    // Update is called once per frame
    void Update()
    {
        currentWeapon = inventory.GetItem(manager.CurrentlyEquippedWeapon());


        if(currentWeapon != null)
        {
            if (currentWeapon.weaponType == WeaponType.AR)
            {
                if(Input.GetKey(KeyCode.Mouse0))
                {
                    //Debug.Log(currentWeapon.weaponSlot);
                    Shoot();
                }
            }
        }

        
    }

    private void Shoot()
    {
        if(Time.time > lastShootTime + currentWeapon.fireRate)
        {
            lastShootTime = Time.time;


            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector3 worldMousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(worldMousePosition3D.x, worldMousePosition3D.y);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * currentWeapon.projectileForce;
            spell.GetComponent<TestProjectile>().damage = Random.Range(currentWeapon.minDamage, currentWeapon.maxDamage);
            spell.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(mousePos - myPos));


            
        }

        

    }



    private void GetReferences()
    {
        inventory = GetComponent<PlayerInventory>();
        manager = GetComponent<EquipmentManager>();
    }
}
