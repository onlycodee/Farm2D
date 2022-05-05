using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    [SerializeField] ItemUI itemUIPrefab;
    List<ItemUI> missionUIItems = new List<ItemUI>();
    
    public void LoadUIICon(List<Item> missonItems)
    {
        DestroyAllOldsMissionUI();
        foreach (var item in missonItems)
        {
            ItemUI itemUI = Instantiate(itemUIPrefab, gameObject.transform);
            itemUI.SetData(item);
            missionUIItems.Add(itemUI);
        }
    }

    private void DestroyAllOldsMissionUI()
    {
        foreach (var uiItem in missionUIItems)
        {
            Destroy(uiItem.gameObject);
        }
        missionUIItems.Clear();
    }

    public void UpdateDisplay(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].number > 0)
            {
                missionUIItems[i].SetNumber(items[i].number);
            } else
            {
                missionUIItems[i].Finished();
            }
        }
    }
}
