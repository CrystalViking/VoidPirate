using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeaponManager : MonoBehaviour, IDataPersistence
{

    [SerializeField]
    public GameObject contentGO;

    [SerializeField]
    public GameObject selectWeaponButtonPrefab;

    [SerializeField]
    private WeaponSO[] weaponSaveLoadList;


    public List<WeaponSO> weaponData = new List<WeaponSO>();
    
    // Start is called before the first frame update
    void Start()
    {
        //PopulateList();
        if (contentGO.transform.childCount > 0)
        {
            foreach (Transform option in contentGO.transform)
            {
                Destroy(option.gameObject);
            }
        }
        DataPersistenceManager.instance.LoadGame();
    }

    void OnEnable()
    {
        if (contentGO.transform.childCount > 0)
        {
            foreach (Transform option in contentGO.transform)
            {
                Destroy(option.gameObject);
            }
        }
        DataPersistenceManager.instance.LoadGame();
    }

    void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CheckPlayersCurrentWeapons()
    {

    }

    public void AddWeaponToList(WeaponSO weaponSO)
    {
        //GameObject selectButton;

        weaponData.Add(weaponSO);
        var selectButton = Instantiate(selectWeaponButtonPrefab);
        selectButton.transform.SetParent(contentGO.transform,false);
        //selectButton.transform.localScale = Vector3.one;
        selectButton.GetComponent<WeaponSelectTemplate>().weaponData = weaponSO;
        selectButton.SetActive(true);
        //RefreshList();
    }

    void PopulateList()
    {
        if(weaponData.Count > 0)
        {
            for(int i = 0; i < weaponData.Count; i++)
            {
                selectWeaponButtonPrefab = Instantiate(selectWeaponButtonPrefab, transform, false);
                selectWeaponButtonPrefab.transform.parent = contentGO.transform;
                selectWeaponButtonPrefab.GetComponent<WeaponSelectTemplate>().weaponData = weaponData[i];
                //selectWeaponButtonPrefab.SetActive(true);
            }



        }
    }


    void RefreshList()
    {
        foreach(Transform option in contentGO.transform)
        {
            Destroy(option.gameObject);
        }
        PopulateList();
    }

    public void LoadData(GameData data)
    {
        string id;
        bool purchased;


        
        

        if (data.primaryWeaponsPurchased.Count > 0)
        {
            for(int i = 0; i < weaponSaveLoadList.Length; i++)
            {
                id = weaponSaveLoadList[i].ItemName;

                data.primaryWeaponsPurchased.TryGetValue(id, out purchased);

                if(purchased)
                {
                    AddWeaponToList(weaponSaveLoadList[i]);
                }
            }
        }
        if (data.secondaryWeaponsPurchased.Count > 0)
        {
            for (int i = 0; i < weaponSaveLoadList.Length; i++)
            {
                id = weaponSaveLoadList[i].ItemName;

                data.secondaryWeaponsPurchased.TryGetValue(id, out purchased);

                if (purchased)
                {
                    AddWeaponToList(weaponSaveLoadList[i]);
                }
            }
        }
    }

    public void SaveData(GameData data)
    {
        
    }
}
