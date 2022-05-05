using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    [SerializeField] MoneyDisplay moneyDisplay;

    private void Awake()
    {
        //moneyDisplay = FindObjectOfType<MoneyDisplay>();
    }

    private void Update()
    {
        //moneyDisplay.SetMoney(Money.Instance.currentMoney);
        //moneyDisplay.SetMoney(Money.Instance.currentMoney);
    }

    public void SetMoneyInShop(int money)
    {
        moneyDisplay.SetMoney(money);
    }

    public void Buy(CropAsset cropAsset)
    {
        if (Money.Instance.currentMoney >= cropAsset.seedPrice)
        {
            Money.Instance.currentMoney -= cropAsset.seedPrice;
            Money.Instance.UpdateTxtMoney();
            SetMoneyInShop(Money.Instance.currentMoney);
            SeedBar seedBar = FindObjectOfType<SeedBar>();
            seedBar.Increment(cropAsset.type);
        }        
    }
}
