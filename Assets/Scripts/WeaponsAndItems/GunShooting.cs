using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour, IWeaponData
{
    //public GameObject projectile;
    public WeaponSO weaponData;
    private GunReloading gunReloading;
    [SerializeField]
    private AudioSource weaponAudioSource;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private bool isActive;

    private IBulletShoot bulletShoot;

    private float lastShootTime = 0;


    [SerializeField] private int currentAmmo, currentAmmoStorage;

    private int magAmmoTemp, storedAmmoTemp;

    [SerializeField] bool magIsEmpty = false;
    [SerializeField] protected bool isReloading = false;
    [SerializeField] protected bool reloadingInterrupted = false;


    private bool canShoot;


    private void OnEnable()
    {
        weaponAudioSource.Stop();
        isReloading = gunReloading.IsReloading();
        if (isReloading)
            reloadingInterrupted = true;
    }

    private void OnDisable()
    {
        if (gunReloading.IsReloading())
            reloadingInterrupted = true;
        else
            reloadingInterrupted = false;
    }

    void Start()
    {
        GetReferences();
        InitAmmo(weaponData);
    }

    void Update()
    {
        isActive = gameObject.activeInHierarchy;
        TakeInput();
        isReloading = gunReloading.IsReloading();
    }

    public WeaponSO GetWeaponData()
    {
        return weaponData;
    }

    public WeaponSlot GetWeaponSlot()
    {
        return weaponData.weaponSlot;
    }

    public WeaponType GetWeaponType()
    {
        return weaponData.weaponType;
    }

    public (int,int) GetAmmoAmountInfo()
    {
        (int, int) ammoAmount = (currentAmmo, currentAmmoStorage);
        return ammoAmount;
    }

    private void ReloadFinished(object sender, EventArgs e)
    {
        magIsEmpty = false;
        currentAmmo = magAmmoTemp;
        currentAmmoStorage = storedAmmoTemp;
        magAmmoTemp = 0;
        storedAmmoTemp = 0;
    }


    protected virtual void TakeInput()
    {
        if (IsActive())
        {
            if (weaponData != null && !isReloading)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Shoot();
                }
            }

            if (isReloading == false || reloadingInterrupted == true)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Reload();
                }
            }
        }
    }

    protected virtual void Shoot()
    {
        if (IfCanShoot())
        {
            if (Time.time > lastShootTime + weaponData.fireRate)
            {
                lastShootTime = Time.time;


                weaponAudioSource.clip = weaponData.shootSound;
                weaponAudioSource.Play();


                bulletShoot.InstantiateBullet(weaponData, firePoint);

                //GameObject spell = Instantiate(weaponData.projectilePrefab, firePoint.position, Quaternion.identity);
                ////Vector3 worldMousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ////Vector2 mousePos = new Vector2(worldMousePosition3D.x, worldMousePosition3D.y);
                ////Vector2 myPos = transform.position;
                ////Vector2 direction = (mousePos - myPos).normalized;
                //spell.GetComponent<Rigidbody2D>().velocity = firePoint.right * weaponData.projectileForce;
                //spell.GetComponent<TestProjectile>().damage = UnityEngine.Random.Range(weaponData.minDamage, weaponData.maxDamage);
                ////spell.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(mousePos - myPos));
                //spell.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(firePoint.right));


                UseAmmo(1, 0);
            }
        }
    }

    public void InitAmmo(WeaponSO weaponData)
    {
        currentAmmo = weaponData.magazineSize;
        currentAmmoStorage = weaponData.storedAmmo;

        magIsEmpty = false;
    }

    private void UseAmmo(int useAmmoInMag, int useAmmoInStorage)
    {
        if (currentAmmo <= 0)
        {
            magIsEmpty = true;
        }
        else
        {
            currentAmmo -= useAmmoInMag;
            currentAmmoStorage -= useAmmoInStorage;

            //update hud event
        }
    }

    public void AddAmmo(int addAmmoInStorage)
    {
        currentAmmoStorage += addAmmoInStorage;

        //update hud event
    }

    private void AddAmmo(int addAmmoInMag, int addAmmoInStorage)
    {
        currentAmmo += addAmmoInMag;
        currentAmmoStorage += addAmmoInStorage;


        //update hud event
    }

    protected bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }

    protected virtual void Reload()
    {
        int mag = weaponData.magazineSize;
        int currentAmmoTemp = currentAmmo;
        int currentAmmoStorageTemp = currentAmmoStorage;

        magAmmoTemp = 0;
        storedAmmoTemp = 0;

        int ammoToReplenish = mag - currentAmmoTemp;
        int ammoReplenished = 0;

        if (currentAmmoStorageTemp > 0)
        {
            if (currentAmmoTemp == 0)
            {
                if (currentAmmoStorageTemp > mag)
                {
                    currentAmmoTemp = mag;
                    currentAmmoStorageTemp -= mag;

                    ammoReplenished = mag;
                }
                else if (currentAmmoStorageTemp <= mag
                        &&
                        currentAmmoStorageTemp > 0)
                {
                    if (currentAmmoStorageTemp >= ammoToReplenish)
                    {
                        currentAmmoTemp = mag;
                        currentAmmoStorageTemp -= ammoToReplenish;

                        ammoReplenished = ammoToReplenish;
                    }
                    else
                    {
                        ammoReplenished = currentAmmoTemp;

                        currentAmmoTemp += currentAmmoStorageTemp;
                        currentAmmoStorageTemp = 0;
                    }
                }
            }
            else if (currentAmmoTemp > 0)
            {
                if (currentAmmoStorageTemp > 0)
                {
                    if (currentAmmoStorageTemp > ammoToReplenish)
                    {
                        currentAmmoTemp = mag;
                        currentAmmoStorageTemp -= ammoToReplenish;

                        ammoReplenished = ammoToReplenish;
                    }
                    else if (currentAmmoStorageTemp <= ammoToReplenish)
                    {
                        ammoReplenished = currentAmmoStorageTemp;

                        currentAmmoTemp += currentAmmoStorageTemp;
                        currentAmmoStorageTemp = 0;
                    }
                }

            }
        }
        else
        {
            Debug.Log("can't reload");
        }


        //Reload animation
        if (gunReloading.IsReloading() == false && currentAmmo < mag && currentAmmoStorage > 0)
        {
            gunReloading.Reload(weaponData);
            magAmmoTemp = currentAmmoTemp;
            storedAmmoTemp = currentAmmoStorageTemp;
        }
        else if (reloadingInterrupted)
        {
            reloadingInterrupted = false;
            gunReloading.Reload(weaponData);
            magAmmoTemp = currentAmmoTemp;
            storedAmmoTemp = currentAmmoStorageTemp;
        }
        //else if(gunReloading.isFinished())
        //{
        //    currentAmmo = currentAmmoTemp;
        //    currentAmmoStorage = currentAmmoStorageTemp;
        //    magIsEmpty = false;
        //}

        //UseAmmo(0, Mathf.Abs(currentAmmoStorage - currentAmmoStorageTemp));
        //AddAmmo(Mathf.Abs(currentAmmo - currentAmmoStorageTemp), 0);

    }

    IEnumerator reloadAfterAnimation(int currentAmmoTemp, int currentAmmoStorageTemp)
    {
        yield return new WaitUntil(() => gunReloading.isFinished());

        currentAmmo = currentAmmoTemp;
        currentAmmoStorage = currentAmmoStorageTemp;
    }

    private bool IfCanShoot()
    {
        if (magIsEmpty || gameObject.activeInHierarchy == false || gunReloading.IsReloading())
            return false;
        else if (!magIsEmpty && gameObject.activeInHierarchy == true)
            return true;
        return false;
    }

    //public static float GetAngleFromVectorFloat(Vector3 dir)
    //{
    //    dir = dir.normalized;
    //    float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //    if (n < 0) n += 360;

    //    return n;
    //}

    private void GetReferences()
    {
        weaponAudioSource = GetComponent<AudioSource>();
        gunReloading = GetComponent<GunReloading>();
        bulletShoot = GetComponent<ImpulseBullet>();
        gunReloading.ReloadFinished += ReloadFinished;
    }

}
