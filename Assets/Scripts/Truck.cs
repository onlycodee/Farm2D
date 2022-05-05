using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour, IReset
{
    [SerializeField] GameObject goodsGrid;
    [SerializeField] CropUI cropUIPrefab;
    [SerializeField] Mission mission;
    [SerializeField] Vector3 initPosition;
    BoxCollider2D collider;
    List<Item> items;
    Transform targetToFollow = null;
    float initDistance = 5;


    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        items = new List<Item>();
        initPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetToFollow != null)
        {
            transform.up = targetToFollow.position - transform.position;
            Vector3 dir = (targetToFollow.position - transform.position).normalized;
            transform.position = targetToFollow.position - dir * initDistance; 
        }
    }

    public void SetTargetToFollow(Transform target)
    {
        //if (target)
        targetToFollow = target;
        if (target == null) return;
        initDistance = Vector2.Distance(target.position, transform.position) + .2f;
        Vector3 dir = (targetToFollow.position - transform.position).normalized;
        transform.position = targetToFollow.position - dir * initDistance;
    }

    public void AddItem(IItemInfo itemToAdd)
    {
        Item foundedItem = items.Find(item => item.GetType() == itemToAdd.GetType());
        if (foundedItem != null)
        {
            foundedItem.number++; 
        } else
        {
            Item newItem = new Item(itemToAdd.GetItemType(), 1, itemToAdd.GetSprite(), itemToAdd.GetPrice());
            items.Add(newItem);
        }
        CropUI cropUI = Instantiate(cropUIPrefab, goodsGrid.transform);
        cropUI.SetImage(itemToAdd.GetSprite());
       // mission.UpdateItems(items);
    }

    public void AddAllItemsToMission()
    {
        mission.UpdateItems(items);
        items.Clear();
        ClearTruck();
    }

    public void SellAllCrops()
    {
        int totalMoneys = 0;
        if (items.Count > 0)
        {
            foreach (var item in items)
            {
                totalMoneys += item.price;
            }
        }
        Money.Instance.AddToMoney(totalMoneys);
        ClearTruck();


    }

    public void ClearTruck()
    {
        CropUI[] crops = goodsGrid.GetComponentsInChildren<CropUI>();
        for (int i = 0; i < crops.Length; i++)
        {
            Destroy(crops[i].gameObject);
        }
    }

    public BoxCollider2D GetCollider()
    {
        return collider;
    }

    public void Reset()
    {
        ClearTruck();
        targetToFollow = null;
        transform.position = initPosition;
        transform.rotation = Quaternion.identity;
    }
}
