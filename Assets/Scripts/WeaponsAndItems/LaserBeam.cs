using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField]
    float damage;


    private void Start()
    {
        damage = GetComponent<LaserShooting>().weaponData.maxDamage;
    }

    public void DealDamage(Collider2D collision)
    {

        StartCoroutine(DamageDelay(0.1f, collision));


        //if (collision.tag == "Enemy" || collision.tag == "Boss")
        //{
        //    if (collision.GetComponent<IEnemy>() != null)
        //    {
        //        collision.GetComponent<IEnemy>().TakeDamage(damage);
                
        //    }
        //    if (collision.GetComponent<EstrellaController>() != null)
        //    {
        //        collision.GetComponent<EstrellaController>().TakeDamage(damage);
        //    }
        //    if (collision.GetComponent<ReaperController>() != null)
        //    {
        //        collision.GetComponent<ReaperController>().TakeDamage(damage);
        //    }
        //    if (collision.GetComponent<MinionController>() != null)
        //    {
        //        collision.GetComponent<MinionController>().TakeDamage(damage);
        //    }
        //}
    }
    

    IEnumerator DamageDelay(float seconds, Collider2D collision)
    {
        yield return new WaitForSeconds(seconds);
        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            if (collision.GetComponent<IEnemy>() != null)
            {
                collision.GetComponent<IEnemy>().TakeDamage(damage);

            }
            if (collision.GetComponent<EstrellaController>() != null)
            {
                collision.GetComponent<EstrellaController>().TakeDamage(damage);
            }
            if (collision.GetComponent<ReaperController>() != null)
            {
                collision.GetComponent<ReaperController>().TakeDamage(damage);
            }
            if (collision.GetComponent<MinionController>() != null)
            {
                collision.GetComponent<MinionController>().TakeDamage(damage);
            }
        }
    }
}
