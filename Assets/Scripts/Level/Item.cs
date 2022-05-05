using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public Item(ItemType type, int number, Sprite icon, int price)
    {
        this.type = type;
        this.number = number;
        this.icon = icon;
        this.price = price;
    }
    [HideInInspector]
    public int price;
    public Sprite icon;
    public ItemType type;
    public int number = 0;
}
