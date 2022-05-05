using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dish")]
public class DishAsset : ScriptableObject, IItemInfo
{
    public Sprite sprite;
    public int price;
    public ItemType type;

    public int GetPrice()
    {
        return price;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    ItemType IItemInfo.GetItemType()
    {
        return type;
    }
}
