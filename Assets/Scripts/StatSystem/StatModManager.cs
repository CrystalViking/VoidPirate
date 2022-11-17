using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModManager : MonoBehaviour
{
    IActorStats actorStats;
    private List<IStatModApplier> modAppliers;
    void Start()
    {
        actorStats = GetComponent<IActorStats>();
        modAppliers = new List<IStatModApplier>();
    }

    
    void Update()
    {
        
    }


    public void AddApplier(IStatModApplier applier)
    {
        //if(!modAppliers.Contains(x => x.ModApplicationType == applier.ModApplicationType))
        //{

        //}
    }

}
