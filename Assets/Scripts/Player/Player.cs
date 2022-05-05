using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour, IReset
{
    [SerializeField] CropAsset cropAsset;
    [SerializeField] ToolAsset toolAsset;
    [SerializeField] SpriteRenderer itemRenderer;
    [SerializeField] SpriteRenderer cropRenderer;
    [SerializeField] Truck truck;
    [SerializeField] CircleCollider2D truckRope;
    [SerializeField] BoxCollider2D leftHand;
    [SerializeField] GameObject target = null;
    [SerializeField] Dish dishPrefab;
    [SerializeField] GameObject explosionFX;
    [SerializeField] GameObject hitFX;
    [SerializeField] SeedBar seedBar;
    [SerializeField] Transform rightHandTransform;
    [SerializeField] NotifyText notifyText;
    [SerializeField] BoxCollider2D weaponCollider;
    [SerializeField] GameObject coinPrefab;
    CircleCollider2D circleCollider;
    Crop holdingCrop;
    Dish holdingDish;
    Animator anim;
    HoldingState holdingState = HoldingState.None;
    Dictionary<string, AIState> _aiStates = new Dictionary<string, AIState>();
    AIState currentState;
    [Header("Layermasks for filter")]
    [SerializeField] LayerMask cropMask;
    [SerializeField] LayerMask cropDishMask;
    ContactFilter2D cropFilter;
    ContactFilter2D cropDishFilter;

    BoxCollider2D sword;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentState = null;
        AIState plant = new PlantState(this);
        AIState watering = new WateringState(this);
        AIState hoe = new HoeState(this);
        AIState attack = new AttackState(this);
        _aiStates.Add(plant.GetName(), plant);
        _aiStates.Add(watering.GetName(), watering);
        _aiStates.Add(hoe.GetName(), hoe);
        _aiStates.Add(attack.GetName(), attack);
        cropFilter.useTriggers = true;
        cropFilter.SetLayerMask(cropMask);
        cropDishFilter.useTriggers = true;
        cropDishFilter.SetLayerMask(cropDishMask);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (holdingState == HoldingState.Crop)
            {
                ProcessCropBehaviour();
            } else if (holdingState == HoldingState.TruckRope)
            {
                ProcessTruckRopeBeharivour();
            }
            else
            {
                ProcessNoHodingItemBehaviour();
            }
        }
    }

    private void ProcessNoHodingItemBehaviour()
    {
        if (circleCollider.IsTouching(truckRope))
        {
            truck.SetTargetToFollow(transform);
            itemRenderer.sprite = null;
            holdingState = HoldingState.TruckRope;
            return;
        }
        List<Collider2D> collidedDishesAndCrops = GetAllCollidersCollideWith(circleCollider, cropDishFilter);
        if (collidedDishesAndCrops.Count > 0)
        {
            foreach (var obj in collidedDishesAndCrops)
            {
                if (obj.GetComponent<Crop>())
                {
                    holdingCrop = obj.GetComponent<Crop>();
                    holdingCrop.StopMoving();
                    obj.transform.SetParent(transform);
                    obj.transform.localPosition = Vector3.zero;
                    itemRenderer.sprite = null;
                    holdingState = HoldingState.Crop;
                    return;
                }
                if (obj.GetComponent<Dish>())
                {
                    holdingDish = obj.GetComponent<Dish>();
                    obj.transform.SetParent(transform);
                    obj.transform.localPosition = Vector3.zero;
                    itemRenderer.sprite = null;
                    holdingState = HoldingState.Crop;
                    return;
                }
            }
        }
    }

    private void ProcessTruckRopeBeharivour()
    {
        truck.SetTargetToFollow(null);
        holdingState = HoldingState.None;
    }

    private void ProcessCropBehaviour()
    {
        holdingState = HoldingState.None;
        if (circleCollider.IsTouching(truck.GetCollider()))
        {
            if (holdingCrop == null)
            {
                truck.AddItem(holdingDish.GetAsset());
                Destroy(holdingDish.gameObject);
                holdingDish = null;
            }
            else
            {
                truck.AddItem(holdingCrop.cropAsset); 
                Destroy(holdingCrop.gameObject);
                holdingCrop = null;
            }
        }
        else if (holdingCrop != null)
        {
            List<Collider2D> collidedCrops = null;
            collidedCrops = GetAllCollidersCollideWith(holdingCrop.GetComponent<BoxCollider2D>(), cropFilter);
            Crop otherCrop = null;
            if (collidedCrops.Count > 0)
            {
                SoundPlayer.Instance.PlayScraftClip();
                GameObject explo = Instantiate(explosionFX, holdingCrop.transform.position, Quaternion.identity);
                Destroy(explo, 1.0f);
                otherCrop = collidedCrops[0].GetComponent<Crop>();
                DishAsset dishAsset = Receipt.Craft(holdingCrop.cropAsset.type,
                    otherCrop.cropAsset.type);
                Dish newDish = Instantiate(dishPrefab);
                newDish.transform.position = holdingCrop.transform.position;
                newDish.SetAsset(dishAsset);
                holdingDish = newDish;
                Destroy(holdingCrop.gameObject);
                Destroy(otherCrop.gameObject);
                holdingCrop = null;
                return;
            }
        }
        if (holdingCrop != null)
        {
            holdingCrop.transform.SetParent(null);
            holdingCrop = null;
        }
        else if (holdingDish != null)
        {
            holdingDish.transform.SetParent(null);
            holdingDish = null;
        }
    }

    private List<Collider2D> GetAllCollidersCollideWith(Collider2D sourceCollider, ContactFilter2D filter)
    {
        List<Collider2D> result = new List<Collider2D>();
        sourceCollider.OverlapCollider(filter, result);
        return result;
    }

    public void EquipTool(ToolAsset toolParam)
    {
        toolAsset = toolParam;
        itemRenderer.sprite = toolAsset.sprite;
        holdingState = HoldingState.Tool;
        if (toolAsset.type == ToolAsset.ToolType.Plow)
        {
            ChangeState("Hoe");
        } else if (toolAsset.type == ToolAsset.ToolType.Watercan)
        {
            ChangeState("Watering");
        } else
        {
            ChangeState("Attack");
        }
    }

    public void EquipCrop(CropAsset cropParam)
    {
        itemRenderer.sprite = cropParam.seedIcon; 
        cropAsset = cropParam;
        holdingState = HoldingState.Seed;
        ChangeState("Plant");
    }

    public void EquipCrop(string cropName)
    {

    }

    public void ChangeState(AIState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
            currentState = newState;
            currentState.OnEnter();
        }
    }

    public void PlantSeed(Dirt dirt)
    {
        int seedRemaining = seedBar.GetSeedNumber(cropAsset.type);
        if (seedRemaining > 0)
        {
            dirt.PlantSeed(cropAsset);
            seedBar.Decrement(cropAsset.type);
        } else
        {
            notifyText.Display();
        }
    }

    public void Watering(Dirt dirt)
    {
        dirt.Water();
        anim.SetTrigger("Work");
    }

    public void Hoe(Dirt dirt)
    {
        anim.SetTrigger("Work");
        dirt.Hoe();
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        LayerMask catLayermark = LayerMask.GetMask("Animal");
        List<Collider2D> result = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(catLayermark);
        filter.useTriggers = true;
        if (weaponCollider.OverlapCollider(filter, result) > 0)
        {
            SoundPlayer.Instance.PlayHitClip();
            GameObject hitFx = Instantiate(hitFX, result[0].transform.position, Quaternion.identity);
            Destroy(hitFx, 1.0f);
            result[0].GetComponent<Animal>().Die();
            Money.Instance.AddToMoney(5);
            GameObject coint = Instantiate(coinPrefab);
            coint.transform.position = result[0].transform.position;
            coint.transform.DOMoveY(transform.position.y + 2, .7f).SetEase(Ease.OutQuad).OnComplete(() => Destroy(coint.gameObject));
        } else
        {
            print("No collide with animal");
        }
    }

    public void ChangeState(string stateName)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        AIState newState = _aiStates[stateName];
        currentState = newState;
        currentState.OnEnter();
    }

    public BoxCollider2D GetLeftHandCollider()
    {
        return leftHand;
    }

    public HoldingState GetHoldingState()
    {
        return holdingState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }

    public void Reset()
    {
        holdingState = HoldingState.None;
        holdingCrop = null;
        holdingDish = null;
    }
}

public enum HoldingState
{
    None, 
    Tool, 
    Seed,
    Crop,
    TruckRope
}
