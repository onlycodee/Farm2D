using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBar : MonoBehaviour, IReset
{
    [SerializeField] List<SeedButton> seedButtons;
    public void Increment(ItemType itemType)
    {
        foreach (var seedButton in seedButtons)
        {
            if (seedButton.GetCropAsset().type == itemType)
            {
                seedButton.SeedCount++;
                seedButton.UpdateSeedCountText();
                return;
            } 
        }
    }

    public int GetSeedNumber(ItemType itemType)
    {
        foreach (var seedButton in seedButtons)
        {
            if (seedButton.GetCropAsset().type == itemType)
            {
                return seedButton.SeedCount;
            }
        }
        return 0;
    }

    public void Decrement(ItemType itemType)
    {
        foreach (var seedButton in seedButtons)
        {
            if (seedButton.GetCropAsset().type == itemType)
            {
                seedButton.SeedCount--;
                seedButton.UpdateSeedCountText();
                return;
            }
        }
    }

    public void Reset()
    {
        print("Reset in seed bar");
        foreach (var seedButton in seedButtons)
        {
            seedButton.SeedCount = 0;
            seedButton.UpdateSeedCountText();
        }
    }
}
