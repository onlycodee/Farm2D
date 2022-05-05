using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Plant")]
public class CropAsset : ScriptableObject, IItemInfo 
{
    public ItemType type;
    public CropStage[] stages;
    public Sprite seedIcon;
    public int price;
    public int seedPrice;

    public int GetPrice()
    {
        return price;
    }

    public Sprite GetSprite()
    {
        return stages[stages.Length - 1].sprite;
    }

    public float GetTotalTimesToGrow()
    {
        float totalTime = 0;
        for (int i = 0; i < stages.Length; i++)
        {
            totalTime += stages[i].timeToGrow;
        }
        return totalTime;
    }

    ItemType IItemInfo.GetItemType()
    {
        return type;
    }
}

[System.Serializable]
public enum ItemType
{
    Tomato,
    Carrot,
    Banana,
    BanhNgot,
    BanhKem,
    Bread,
    DuiGa,
    ComCuon,
    Humber
}

[System.Serializable]
public struct CropStage
{
    public Sprite sprite;
    public float timeToGrow;
}
