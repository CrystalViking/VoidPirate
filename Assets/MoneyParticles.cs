using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyParticles : MonoBehaviour
{
    public ParticleSystem particles;
    private ParticleSystem.MainModule _main;
    [Header("Music")]
    public AudioSource audioSource;
    public float volume = 1;

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
            audioSource.volume = volume;
            audioSource.Play();
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
