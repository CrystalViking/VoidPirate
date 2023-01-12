using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLoadManager : MonoBehaviour, IDataPersistence
{

    [SerializeField]
    private GameObject primaryShopContent;
    private GameObject secondaryShopContent;
    
    List<SavableBuyOption> optionsPrimary = new List<SavableBuyOption>();
    List<SavableBuyOption> optionsSecondary = new List<SavableBuyOption>();



    // Start is called before the first frame update
    void Start()
    {
        //GetComponentInChildren<ShopCoinManager>().PurchaseFinished += 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        optionsPrimary = data.buyOptionsPrimary;
        optionsSecondary = data.buyOptionsSecondary;

        //for loop enabling/disabling buy options


    }

    public void SaveData(GameData data)
    {
        //for loop scanning for ids of weapon shop list


    }

    private void PurchaseFinished(object sender, ItemPurchasedEventArgs e)
    {

    }    
}
