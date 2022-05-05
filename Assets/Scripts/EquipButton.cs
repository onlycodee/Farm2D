using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
    [SerializeField] ToolAsset tool;
    [SerializeField] KeyCode equipKey;
    public void SetPlayerTool()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
        {
            print("Failed to find player");
        }
        Player playerComp = playerObj.GetComponent<Player>();
        playerComp.EquipTool(tool);
    }

    private void Update()
    {
        if (Input.GetKeyDown(equipKey))
        {
            SetPlayerTool();
        }
    }
}
