using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyDisplay : MonoBehaviour
{
    Text txtMoney;

    private void Awake()
    {
        txtMoney = GetComponent<Text>();
    }
    
    public void SetMoney(int money)
    {
        if (txtMoney == null)
        {
            txtMoney = GetComponent<Text>();
        }
        txtMoney.text = money.ToString();
    }
}
