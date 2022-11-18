using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatModManager : MonoBehaviour
{
    private List<IStatModApplier> modAppliers;
    void Start()
    {
        modAppliers = new List<IStatModApplier>();
    }

    
    void Update()
    {
        RemoveExpiredAppliers();
    }

    public void AddApplier(IStatModApplier applier, float seconds)
    {
        if (!modAppliers.Any(x => x.ModApplicationType == applier.ModApplicationType))
        {
            modAppliers.Add(applier);
            applier.ApplyModifierForSeconds(seconds);
        }
    }

    public void RemoveExpiredAppliers()
    {
        if(modAppliers.Count > 0)
        {
            for(int i = 0; i < modAppliers.Count; i++)
            {
                if (modAppliers[i].Expired)
                {
                    modAppliers.Remove(modAppliers[i]);
                }

            }
        }
    }

}
