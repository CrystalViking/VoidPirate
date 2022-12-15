using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyParticles : MonoBehaviour
{
    public ParticleSystem particles;
    private ParticleSystem.MainModule _main;
    void Start()
    {
        _main = particles.main;
        StartCoroutine(ThingsLater());
    }

    public void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerCurrency>().AddCoins(1);
        }
    }
    void Update()
    {
        
    }

    IEnumerator ThingsLater()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }
}
