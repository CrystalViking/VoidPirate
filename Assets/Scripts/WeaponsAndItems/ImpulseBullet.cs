using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseBullet : MonoBehaviour, IBulletShoot
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void InstantiateBullet(WeaponSO weaponData, Transform firePoint)
    {
        GameObject spell = Instantiate(weaponData.projectilePrefab, firePoint.position, Quaternion.identity);
        spell.GetComponent<Rigidbody2D>().velocity = firePoint.right * weaponData.projectileForce;
        spell.GetComponent<IProjectile>().Damage = UnityEngine.Random.Range(weaponData.minDamage, weaponData.maxDamage);
        spell.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(firePoint.right));
    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

}
