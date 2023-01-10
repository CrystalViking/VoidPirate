using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyParticles : MonoBehaviour
{
    public ParticleSystem particles;
    private ParticleSystem.MainModule _main;
    private float time = 0;
    [Header("Music")]
    public AudioSource audioSource;
    public float volume = 1;

    void Start()
    {
        _main = particles.main;
        time = Time.time + 5.0f;
    }

    private void Update()
    {
        if(time < Time.time)
        {
            if(particles.particleCount <= 0)
            {
                StartCoroutine(ThingsLater());
            }
        }
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

    IEnumerator ThingsLater()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

}
