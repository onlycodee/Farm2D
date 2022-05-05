using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool")]
public class ToolAsset : ScriptableObject
{   
    public enum ToolType
    {
        Watercan,
        Plow,
        Sword
    }
    public Sprite sprite;
    public ToolType type;
}




