using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIdentification : MonoBehaviour
{
    
    public WeaponIdentificationData[] weaponIds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class WeaponIdentificationData
{
    [SerializeField] public string id;
    public GameObject prefab;

    //public WeaponIdentificationData()
    //{
    //    id = prefab.GetComponent<IWeaponData>().GetWeaponData().ItemName; 
    //}
}
