using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public GameObject projectile;
    private WeaponSO currentWeapon;
    public AudioSource pWeapon;
    public AudioSource sWeapon;

    
    private PlayerInventory playerInventory;
    private EquipmentManager equipmentManager;
    private HudScript hud;

    private float lastShootTime = 0;

    [SerializeField] private int primaryCurrentAmmo;
    [SerializeField] private int primaryCurrentAmmoStorage;

    [SerializeField] private int secondaryCurrentAmmo;
    [SerializeField] private int secondaryCurrentAmmoStorage;

    [SerializeField] private bool primaryMagIsEmpty = false;
    [SerializeField] private bool secondaryMagIsEmpty = false;


    [SerializeField] private bool canShoot;

    

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

        //StartCoroutine(LetOthersCatchUp());

        currentWeapon = playerInventory.GetItem(equipmentManager.CurrentlyEquippedWeapon());

    }

    IEnumerator LetOthersCatchUp()
    {
        yield return new WaitForSeconds(1);
    }


    // Update is called once per frame
    void Update()
    {
        currentWeapon = playerInventory.GetItem(equipmentManager.CurrentlyEquippedWeapon());


        if(currentWeapon != null)
        {
            if (currentWeapon.weaponType == WeaponType.AR || currentWeapon.weaponType == WeaponType.AutoSniper)
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
            Reload(equipmentManager.CurrentlyEquippedWeapon());
        }

        
    }

    private void Shoot()
    {
        IfCanShoot(equipmentManager.CurrentlyEquippedWeapon());


        if(canShoot)
        {
            if (Time.time > lastShootTime + currentWeapon.fireRate)
            {
                lastShootTime = Time.time;

                if(currentWeapon.ItemName == "Impulse Rifle")
                    pWeapon.Play();
                if (currentWeapon.ItemName == "DMR")
                    sWeapon.Play();
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

    public void InitAmmo(int slot, WeaponSO weapon)
    {
        // primary
        if(slot == 0)
        {
            primaryCurrentAmmo = weapon.magazineSize;
            primaryCurrentAmmoStorage = weapon.storedAmmo;
            primaryMagIsEmpty = false;
            equipmentManager.EM_UpdatePrimaryAmmo(primaryCurrentAmmo, primaryCurrentAmmoStorage);
        }

        // secondary
        if (slot == 1)
        {         
            secondaryCurrentAmmo = weapon.magazineSize;
            secondaryCurrentAmmoStorage = weapon.storedAmmo;
            secondaryMagIsEmpty = false;
            equipmentManager.EM_UpdateSecondaryAmmo(secondaryCurrentAmmo, secondaryCurrentAmmoStorage);
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
                IfCanShoot(equipmentManager.CurrentlyEquippedWeapon());
            }
            else
            {
                primaryCurrentAmmo -= currentAmmoUsed;
                primaryCurrentAmmoStorage -= currentStoredAmmoUsed;
                equipmentManager.EM_UpdatePrimaryAmmo(primaryCurrentAmmo, primaryCurrentAmmoStorage);
                hud.UpdateWeaponAmmoInfo(primaryCurrentAmmo, primaryCurrentAmmoStorage);
            }
            
        }


        // secondary
        if(slot == 1)
        {
            if (secondaryCurrentAmmo <= 0)
            {
                secondaryMagIsEmpty = true;
                IfCanShoot(equipmentManager.CurrentlyEquippedWeapon());
            }
            else
            {
                secondaryCurrentAmmo -= currentAmmoUsed;
                secondaryCurrentAmmoStorage -= currentStoredAmmoUsed;
                equipmentManager.EM_UpdateSecondaryAmmo(secondaryCurrentAmmo, secondaryCurrentAmmoStorage);
                hud.UpdateWeaponAmmoInfo(secondaryCurrentAmmo, secondaryCurrentAmmoStorage);
            }
        }
    }


    private void AddAmmo(int slot, int currentAmmoAdded, int currentStoredAmmoAdded)
    {
        if(slot == 0)
        {
            primaryCurrentAmmo += currentAmmoAdded;
            primaryCurrentAmmoStorage += currentStoredAmmoAdded;
            if(equipmentManager.CurrentlyEquippedWeapon() == slot)
                hud.UpdateWeaponAmmoInfo(primaryCurrentAmmo, primaryCurrentAmmoStorage);
            equipmentManager.EM_UpdatePrimaryAmmo(primaryCurrentAmmo, primaryCurrentAmmoStorage);
        }
        if(slot == 1)
        {
            secondaryCurrentAmmo += currentAmmoAdded;
            secondaryCurrentAmmoStorage += currentStoredAmmoAdded;
            if (equipmentManager.CurrentlyEquippedWeapon() == slot)
                hud.UpdateWeaponAmmoInfo(secondaryCurrentAmmo, secondaryCurrentAmmoStorage);
            equipmentManager.EM_UpdateSecondaryAmmo(secondaryCurrentAmmo, secondaryCurrentAmmoStorage);
        }
        
    }

    public void AddAmmoToCurrentWeaponType(WeaponType weaponType, int currentStoredAmmoAdded)
    {
        for(int i = 0; i < playerInventory.GetInventorySize(); ++i)
        {
            if(playerInventory.GetItem(i)?.weaponType == weaponType)
            {
                AddAmmo((int)playerInventory.GetItem(i).weaponSlot, 0, currentStoredAmmoAdded);
            }
        }
    }

    private void Reload(int slot)
    {

        int mag = playerInventory.GetItem(slot).magazineSize;
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
        int ammoReplenished = 0;

        if(currentSlotAmmoLeft > 0)
        {
            if (currentSlotAmmo == 0)
            {
                if (currentSlotAmmoLeft > mag)
                {
                    currentSlotAmmo = mag;
                    currentSlotAmmoLeft -= mag;

                    ammoReplenished = mag;
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

                        ammoReplenished = ammoToReplenish;

                    }
                    else
                    {
                        ammoReplenished = currentSlotAmmoLeft;

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
                        currentSlotAmmo = playerInventory.GetItem(slot).magazineSize;
                        currentSlotAmmoLeft -= ammoToReplenish;

                        ammoReplenished = ammoToReplenish;
                    }
                    else if (currentSlotAmmoLeft <= ammoToReplenish)
                    {
                        ammoReplenished = currentSlotAmmoLeft;

                        currentSlotAmmo += currentSlotAmmoLeft;
                        currentSlotAmmoLeft = 0;
                    }
                    
                }
            }
            currentMagIsEmpty = false;
            //UseAmmo(slot, 0, ammoReplenished);
            //AddAmmo(slot, ammoReplenished, 0);
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

            UseAmmo(slot, 0, Mathf.Abs(primaryCurrentAmmoStorage - currentSlotAmmoLeft));
            AddAmmo(slot, Mathf.Abs(primaryCurrentAmmo - currentSlotAmmo), 0);
        }
        if (slot == 1)
        {
            secondaryCurrentAmmo = currentSlotAmmo;
            secondaryCurrentAmmoStorage = currentSlotAmmoLeft;
            secondaryMagIsEmpty = currentMagIsEmpty;

            UseAmmo(slot, 0, Mathf.Abs(secondaryCurrentAmmoStorage - currentSlotAmmoLeft));
            AddAmmo(slot, Mathf.Abs(secondaryCurrentAmmo - currentSlotAmmo), 0);
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
        
        playerInventory = GetComponent<PlayerInventory>();
        equipmentManager = GetComponent<EquipmentManager>();
        hud = GetComponent<HudScript>();
    }
}
