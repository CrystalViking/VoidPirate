using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitInventory : MonoBehaviour  //, IPlayerInventory
{
    [SerializeField]
    List<ScriptableMedkit> medkits;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddStack(ScriptableMedkit newItem)
    {
        if(medkits == null && medkits.Count < 10)
            medkits.Add(newItem);
    }

    

    public void UseStack()
    {
        if(medkits.Count > 0)
        {
            HealPlayer(medkits[medkits.Count-1]);
            medkits.Remove(medkits[medkits.Count-1]);
        }     
    }

    public int GetStackSize()
    {
        if(medkits != null)
        {
            return medkits.Count;
        }
        return 0;
    }

    private void HealPlayer(ScriptableMedkit medkit)
    {
        GetComponent<PlayerStats>().Heal(medkit.hp_content);
    }

}
