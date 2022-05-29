using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public GameObject projectile;
    private Weapon currentWeapon;

    private float lastShootTime = 0;

    [SerializeField] private int primaryCurrentAmmo;
    [SerializeField] private int primaryCurrentAmmoStorage;

    [SerializeField] private int secondaryCurrentAmmo;
    [SerializeField] private int secondaryCurrentAmmoStorage;

    [SerializeField] private bool primaryMagIsEmpty = false;
    [SerializeField] private bool secondaryMagIsEmpty = false;


    [SerializeField] private bool canShoot;

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
        canShoot = true;
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

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload(manager.CurrentlyEquippedWeapon());
        }

        
    }

    private void Shoot()
    {
        IfCanShoot(manager.CurrentlyEquippedWeapon());


        if(canShoot)
        {
            if (Time.time > lastShootTime + currentWeapon.fireRate)
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


                UseAmmo((int)currentWeapon.weaponSlot, 1, 0);

            }
        }
        


    }

    public void InitAmmo(int slot, Weapon weapon)
    {
        // primary
        if(slot == 0)
        {
            primaryCurrentAmmo = weapon.magazineSize;
            primaryCurrentAmmoStorage = weapon.storedAmmo;
            primaryMagIsEmpty = false;
        }

        // secondary
        if (slot == 1)
        {
            secondaryCurrentAmmo = weapon.magazineSize;
            secondaryCurrentAmmoStorage = weapon.storedAmmo;
            secondaryMagIsEmpty = false;
        }
    }

    private void UseAmmo(int slot, int currentAmmoUsed, int currentStoredAmmoUsed)
    {
        // primary
        if(slot == 0)
        {
            if(primaryCurrentAmmo <= 0)
            {
                primaryMagIsEmpty = true;
                IfCanShoot(manager.CurrentlyEquippedWeapon());
            }
            else
            {
                primaryCurrentAmmo -= currentAmmoUsed;
                primaryCurrentAmmoStorage -= currentStoredAmmoUsed;
            }
            
        }


        // secondary
        if(slot == 1)
        {
            if (secondaryCurrentAmmo <= 0)
            {
                secondaryMagIsEmpty = true;
                IfCanShoot(manager.CurrentlyEquippedWeapon());
            }
            else
            {
                secondaryCurrentAmmo -= currentAmmoUsed;
                secondaryCurrentAmmoStorage -= currentStoredAmmoUsed;
            }
        }
    }


    private void Reload(int slot)
    {

        int mag = inventory.GetItem(slot).magazineSize;
        int currentSlotAmmo = 0;
        int currentSlotAmmoLeft = 0;
        bool currentMagIsEmpty = true;
        
        if (slot == 0)
        {
            currentSlotAmmo = primaryCurrentAmmo;
            currentSlotAmmoLeft = primaryCurrentAmmoStorage;
            currentMagIsEmpty = primaryMagIsEmpty;
        }
        if (slot == 1)
        {
            currentSlotAmmo = secondaryCurrentAmmo;
            currentSlotAmmoLeft = secondaryCurrentAmmoStorage;
            currentMagIsEmpty = secondaryMagIsEmpty;
        }

        int ammoToReplenish = mag - currentSlotAmmo;

        if(currentSlotAmmoLeft > 0)
        {
            if (currentSlotAmmo == 0)
            {
                if (currentSlotAmmoLeft > mag)
                {
                    currentSlotAmmo = mag;
                    currentSlotAmmoLeft -= mag;
                }
                else if (
                        (currentSlotAmmoLeft <= mag)
                        &&
                        currentSlotAmmoLeft > 0)
                {
                    if (currentSlotAmmoLeft >= ammoToReplenish)
                    {
                        currentSlotAmmo = mag;
                        currentSlotAmmoLeft -= ammoToReplenish;

                    }
                    else
                    {
                        currentSlotAmmo += currentSlotAmmoLeft;
                        currentSlotAmmoLeft = 0;
                    }
                }
            }
            else if (currentSlotAmmo > 0)
            {
                if (currentSlotAmmoLeft > 0)
                {
                    if (currentSlotAmmoLeft > ammoToReplenish)
                    {
                        currentSlotAmmo = inventory.GetItem(slot).magazineSize;
                        currentSlotAmmoLeft -= ammoToReplenish;
                    }
                    else if (currentSlotAmmoLeft <= ammoToReplenish)
                    {
                        currentSlotAmmo += currentSlotAmmoLeft;
                        currentSlotAmmoLeft = 0;
                    }
                    
                }
            }
            currentMagIsEmpty = false;
        }
        else
        {          
            Debug.Log("no stored ammo left");            
        }

        if (slot == 0)
        {
            primaryCurrentAmmo = currentSlotAmmo;
            primaryCurrentAmmoStorage = currentSlotAmmoLeft;
            primaryMagIsEmpty = currentMagIsEmpty;
        }
        if (slot == 1)
        {
            secondaryCurrentAmmo = currentSlotAmmo;
            secondaryCurrentAmmoStorage = currentSlotAmmoLeft;
            secondaryMagIsEmpty = currentMagIsEmpty;
        }

    }

    private void IfCanShoot(int slot)
    {
        if(slot == 0)
        {
            if (primaryMagIsEmpty)
            {
                canShoot = false;
            }
            else
            {
                canShoot = true;
            }
        }

        if(slot == 1)
        {
            if (secondaryMagIsEmpty)
            {
                canShoot = false;
            }
            else
            {
                canShoot = true;
            }
        }

        
    }

    private void GetReferences()
    {
        inventory = GetComponent<PlayerInventory>();
        manager = GetComponent<EquipmentManager>();
    }
}
