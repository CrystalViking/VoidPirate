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

            onMedkitStackChanged?.Invoke(scriptableMedkits.Count.ToString());
        }
    }

    public void UseMedkit()
    {
        if(scriptableMedkits != null && scriptableMedkits.Count > 0)
        {
            HealPlayer(scriptableMedkits.Last());
            scriptableMedkits.RemoveAt(scriptableMedkits.Count - 1);

            onMedkitStackChanged?.Invoke(scriptableMedkits.Count.ToString());
        }
    }


    public void UseEnergyDrink()
    {
        if(scriptableEnergyDrinks != null && scriptableEnergyDrinks.Count > 0)
        {
            EnergizePlayer(scriptableEnergyDrinks.Last());
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

            onEnergyStackChanged?.Invoke(scriptableEnergyDrinks.Count.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        onMedkitStackChanged?.Invoke(scriptableMedkits.Count.ToString());
        onEnergyStackChanged?.Invoke(scriptableEnergyDrinks.Count.ToString());

        if (Input.GetKey(KeyCode.Alpha5))
        {
            
            StartCoroutine(UseMedkitWithCoolDown());
            
        }

        if (Input.GetKey(KeyCode.Alpha6))
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
