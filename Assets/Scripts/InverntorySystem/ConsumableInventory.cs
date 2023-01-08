using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ConsumableInventory : MonoBehaviour
{
    [SerializeField] private List<ScriptableMedkit> scriptableMedkits;
    [SerializeField] private List<ScriptableEnergyDrink> scriptableEnergyDrinks;

    private bool medkitOnCooldown = false;
    private bool energyDrinkOnCooldown = false;


    [SerializeField]
    private UnityEvent<string> onEnergyStackChanged;
    [SerializeField]
    private UnityEvent<string> onMedkitStackChanged;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip medkitUsage;
    public AudioClip energeticUsage;
    public AudioClip itemPickup;

    void Start()
    {
        scriptableMedkits = new List<ScriptableMedkit>();
        scriptableEnergyDrinks = new List<ScriptableEnergyDrink>();
    }


    public void AddMedkit(ScriptableMedkit medkit)
    {
        if(scriptableMedkits != null && scriptableMedkits.Count < 10)
        {
            scriptableMedkits.Add(medkit);
            audioSource.clip = itemPickup;
            audioSource.volume = 0.6f;
            audioSource.Play();
            onMedkitStackChanged?.Invoke(scriptableMedkits.Count.ToString());
        }
    }

    public void UseMedkit()
    {
        if(scriptableMedkits != null && scriptableMedkits.Count > 0)
        {
            HealPlayer(scriptableMedkits.Last());
            audioSource.clip = medkitUsage;
            audioSource.volume = 0.5f;
            audioSource.Play();
            scriptableMedkits.RemoveAt(scriptableMedkits.Count - 1);

            onMedkitStackChanged?.Invoke(scriptableMedkits.Count.ToString());
        }
    }


    public void UseEnergyDrink()
    {
        if(scriptableEnergyDrinks != null && scriptableEnergyDrinks.Count > 0)
        {
            EnergizePlayer(scriptableEnergyDrinks.Last());
            audioSource.clip = energeticUsage;
            audioSource.volume = 0.3f;
            audioSource.Play();
            scriptableEnergyDrinks.RemoveAt(scriptableEnergyDrinks.Count - 1);

            onEnergyStackChanged?.Invoke(scriptableEnergyDrinks.Count.ToString());
        }
    }

    private void EnergizePlayer(ScriptableEnergyDrink energyDrink)
    {
        GetComponentInParent<PlayerStats>().Heal(energyDrink.hp_content);
        GetComponentInParent<PlayerStats>().SpeedBuffSecondsPercentAdd(StatModApplicationType.ConsumableAppliedBuff, energyDrink.percentBuff, energyDrink.duration);
    }

    private void HealPlayer(ScriptableMedkit medkit)
    {
        GetComponentInParent<PlayerStats>().Heal(medkit.hp_content);
    }

    public void AddEnergyDrink(ScriptableEnergyDrink energyDrink)
    {
        if (scriptableEnergyDrinks != null && scriptableEnergyDrinks.Count < 10)
        {
            scriptableEnergyDrinks.Add(energyDrink);
            audioSource.clip = itemPickup;
            audioSource.volume = 0.6f;
            audioSource.Play();
            onEnergyStackChanged?.Invoke(scriptableEnergyDrinks.Count.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        onMedkitStackChanged?.Invoke(scriptableMedkits.Count.ToString());
        onEnergyStackChanged?.Invoke(scriptableEnergyDrinks.Count.ToString());

        if (Input.GetKey(KeyCode.LeftControl))
        {
            
            StartCoroutine(UseMedkitWithCoolDown());
            
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(UseEnergyDrinkWithCoolDown());
            
        }
    }

    IEnumerator UseMedkitWithCoolDown()
    {
        if(!medkitOnCooldown)
        {
            medkitOnCooldown = true;
            UseMedkit();
            yield return new WaitForSeconds(1f);
            medkitOnCooldown = false;
        }
    }

    IEnumerator UseEnergyDrinkWithCoolDown()
    {
        if (!energyDrinkOnCooldown)
        {
            energyDrinkOnCooldown = true;
            UseEnergyDrink();
            yield return new WaitForSeconds(5.5f);
            energyDrinkOnCooldown = false;

        }
    }

}
