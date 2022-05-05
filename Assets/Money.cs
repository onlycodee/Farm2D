using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] Text txtMoney;
    private Money _instance;
    public static Money Instance = null;
    public int currentMoney;
    private void Awake()
    {
        if (FindObjectsOfType<Money>().Length > 1)
        {
            Destroy(this);
        }
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateTxtMoney()
    {
        txtMoney.text = currentMoney.ToString();
    }

    public void AddToMoney(int moneyToAdd)
    {
        currentMoney += moneyToAdd;
        UpdateTxtMoney();
    }
}
