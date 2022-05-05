using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemInfo
{
    Sprite GetSprite();
    int GetPrice();
    ItemType GetItemType();
}
