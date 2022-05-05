using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level data")]
public class LevelAsset : ScriptableObject
{
    public List<Item> items;
    public float time;
    public int startingMoney;
}
