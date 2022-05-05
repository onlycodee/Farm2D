using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dirt : MonoBehaviour, IPickupable, IReset
{
    [SerializeField] CropAsset currentCropAsset = null;
    [SerializeField] DirtState state = DirtState.Normal;
    [SerializeField] Sprite normalDirtSprite;
    [SerializeField] Sprite thirstyDirtSprite;
    [SerializeField] Sprite dirtAfterHarvestingSprite;
    [SerializeField] Sprite brokenCrop;
    [SerializeField] Crop cropPrefab;
    [SerializeField] SpriteRenderer brokenCropRenderer;
    //[SerializeField] int a;

    Crop grewCrop = null;
    SpriteRenderer dirtRenderer;


    float growthTimer = 0;
    int currentProgress = 0;

    private void Awake()
    {
        dirtRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dirtRenderer.sprite = normalDirtSprite;
        if (currentCropAsset != null)
        {
            //plantRenderer.sprite = currentPlant.avatars[currentProgress];
            //grownTimer = currentPlant.timesToGrow[currentProgress];
        }
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void PlantSeed(CropAsset crop)
    {
        if (IsPlantable())
        {
            currentCropAsset = crop;
            Crop newCrop = Instantiate(cropPrefab);
            newCrop.transform.position = transform.position;
            newCrop.SetCrop(crop);
            newCrop.SetDirt(this);
            newCrop.GetComponent<BoxCollider2D>().enabled = false;
            grewCrop = newCrop;
        }
    }

    public bool IsPlantable()
    {
        return grewCrop == null && state == DirtState.Normal;
    }

    public void Hoe()
    {
        if (state == DirtState.Broken)
        {
            grewCrop = null;
            brokenCropRenderer.sprite = null;
            state = DirtState.Normal;
            dirtRenderer.sprite = normalDirtSprite;
        }
    }
    
    public void Water()
    {
        if (state == DirtState.Thirsty)
        {
            state = DirtState.Normal;
            dirtRenderer.sprite = normalDirtSprite;
        }
    }

    public bool IsNormal()
    {
        return state == DirtState.Normal;
    }

    public void CropDone()
    {
        state = DirtState.Broken;
        brokenCropRenderer.sprite = brokenCrop;
        dirtRenderer.sprite = dirtAfterHarvestingSprite;

    }

    public void SetDirtThirsty()
    {
        state = DirtState.Thirsty;
        dirtRenderer.sprite = thirstyDirtSprite;
    }

    public void Reset()
    {
        if (state != DirtState.Normal)
        {
            brokenCropRenderer.sprite = null;
            state = DirtState.Normal;
            dirtRenderer.sprite = normalDirtSprite;
        }
    }
}


public enum DirtState
{
    Normal,
    Thirsty,
    Broken
}