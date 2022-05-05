using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Crop : MonoBehaviour, IReset
{
    [SerializeField] float maxDistanceToDrop = 2.0f;
    public CropAsset cropAsset;
    Dirt dirt;
    SpriteRenderer renderer;
    bool isPlanted = false;
    bool isFullyGrown = false;
    float growTimer = 0;
    int currentStageIndex = 0;
    bool onDirt = true;
    float thirstyTime = 0;
    bool hasAThirsty = false;

    Tween tween = null;
    public bool IsFullyGrown()
    {
        return isFullyGrown;
    }

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
        if (isPlanted && !isFullyGrown && dirt.IsNormal())
        {
            growTimer += Time.deltaTime;
            if (growTimer >= thirstyTime && !hasAThirsty)
            {
                hasAThirsty = true;
                dirt.SetDirtThirsty();
                return;
            }
            if (currentStageIndex == cropAsset.stages.Length)
            { 
                isFullyGrown = true;
                GetComponent<BoxCollider2D>().enabled = true;
                return;
            }
            if (currentStageIndex < cropAsset.stages.Length && growTimer >= cropAsset.stages[currentStageIndex].timeToGrow)
            {
                growTimer = 0;
                renderer.sprite = cropAsset.stages[currentStageIndex].sprite;
                currentStageIndex++;
            }        
        }
    }

    public void SetOnDirt(bool state)
    {
        onDirt = state;
    }

    public void SetCrop(CropAsset crop)
    {
        cropAsset = crop;
        renderer.sprite = crop.stages[0].sprite;
        isPlanted = true;
        thirstyTime = UnityEngine.Random.Range(1, cropAsset.GetTotalTimesToGrow());
    }

    public void SetDirt(Dirt dirtParam)
    {
        dirt = dirtParam;
    }

    public Dirt GetDirt()
    {
        return dirt;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onDirt && isFullyGrown && collision.gameObject.GetComponent<Player>())
        {
            onDirt = false;
            dirt.CropDone();
            Vector2 randomDir = UnityEngine.Random.insideUnitCircle;
            Vector2 targetPosition = (Vector2)transform.position + UnityEngine.Random.Range(2, 3) * randomDir;
            tween = transform.DOMove(targetPosition, 1f);
        }
    }

    public void StopMoving()
    {
        if (tween != null)
        {
            tween.Kill();
            tween = null;
        }
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}
