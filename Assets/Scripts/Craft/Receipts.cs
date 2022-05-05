using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipt 
{
    //static Dish[,] receipts = new Dish[4, 4];
    public static string[,] receipts = {
        { "Bread", "BanhNgot", "BanhKem" },
        { "BanhNgot", "ComCuon", "DuiGa" },
        { "BanhKem", "DuiGa", "Humber" }
    };

    public static DishAsset Craft(ItemType cropA, ItemType cropB)
    {
        string stringDish = receipts[(int)cropA, (int)cropB];
        Debug.Log("String dish: " + stringDish);
        DishAsset dish = Resources.Load<DishAsset>(stringDish);
        return dish;
    }

    public enum DishType
    {
        A,
        B,
        C,
        D,
        E,
        F
    }
}
