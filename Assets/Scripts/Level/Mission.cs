using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Mission : MonoBehaviour
{
    [SerializeField] LevelAsset currentLevel;
    [SerializeField] MissionUI missionUI;
    [SerializeField] LevelTimer levelTimer;

    public List<Item> finishedItems = new List<Item>();
    public List<Item> levelItems = new List<Item>();
    

    private void Start()
    {
        //ResetData();
    }

    public void ResetData()
    {
        levelItems = currentLevel.items.ConvertAll(item => new Item(item.type, item.number, item.icon, item.price));
        missionUI.LoadUIICon(levelItems);
        levelTimer.StopCO();
        levelTimer.SetTime(currentLevel.time);
        levelTimer.StartCoutingDown();
        Money.Instance.currentMoney = currentLevel.startingMoney;
        Money.Instance.UpdateTxtMoney();
    }

    public void UpdateItems(List<Item> finishedItemParam)
    {
        finishedItems = finishedItemParam;
        foreach (var finishedItem in finishedItems)
        {
            foreach (var levelItem in levelItems)
            {
                if (finishedItem.type == levelItem.type)
                {
                    levelItem.number -= finishedItem.number;
                }
            }
        }
        missionUI.UpdateDisplay(levelItems);
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        bool isWin = true;
        foreach (var item in levelItems)
        {
            if (item.number > 0)
            {
                isWin = false;
                break;
            }
        }
        if (isWin)
        {
            LevelManager.Instance.DisplayWinDialog();
        }
    }

    public void SetLevel(LevelAsset levelParam)
    {
        this.currentLevel = levelParam;
    }
}
