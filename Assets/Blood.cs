using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public GameObject particle;
    public float bHealth;
    public float aHealth;
    public AudioSource audioSource;
    private List<GameObject> parts;

    public void Start()
    {
        parts = new List<GameObject>();
        bHealth = GetComponent<PlayerStats>().GetMaxHealth();
    }

    public void Update()
    {     
        aHealth = GetComponent<PlayerStats>().GetHealth();
        if (aHealth < bHealth)
        {
            bHealth = aHealth;
            CreateBlood();
        }
        else if (aHealth > bHealth)
        {
            bHealth = aHealth;
        }
        try
        {
            if (parts.Count > 0)
            {
                try
                {
                    foreach (GameObject part in parts)
                    {
                        part.transform.position = transform.position;
                        if (!part.GetComponent<ParticleSystem>().isPlaying)
                        {

                            parts.Remove(part);
                            Destroy(part);

                        }
                    }
                }
                catch 
                {
                    //parts[0].transform.position = transform.position;
                }
            }
        }
        catch { }
        
    }

    public void CreateBlood()
    {
        audioSource.Play();
        try
        {
            parts.Add(Instantiate(particle, transform.position, Quaternion.identity));
        }
        catch { }
            
    }
  
}
