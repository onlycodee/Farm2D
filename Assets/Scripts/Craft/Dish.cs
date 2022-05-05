using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour, IPickupable, IReset
{
    SpriteRenderer renderer;
    DishAsset dishAsset;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DishAsset GetAsset()
    {
        return dishAsset;
    }

    public void SetAsset(DishAsset asset)
    {
        if (asset == null)
        {
            print("Asset in dish is null");
        }
        dishAsset = asset;
        renderer.sprite = dishAsset.GetSprite();
    }

    public void SetSprite(Sprite sprite)
    {
        renderer.sprite = sprite;
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}
