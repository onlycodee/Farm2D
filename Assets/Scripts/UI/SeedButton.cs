using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SeedButton : MonoBehaviour
{
    [SerializeField] CropAsset crop;
    [SerializeField] KeyCode equipKey;
    [SerializeField] Text txtSeedCount;
    public int SeedCount { get; set; }
    private void Update()
    {
        if (Input.GetKeyDown(equipKey))
        {
            EquipSeedForPlayer();
        }
    }

    public void EquipSeedForPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        Player playerComp = playerObj.GetComponent<Player>();
        playerComp.EquipCrop(crop);
    }

    public void UpdateSeedCountText()
    {
        txtSeedCount.text = SeedCount.ToString();
    }

    public CropAsset GetCropAsset()
    {
        return crop;
    }
}
