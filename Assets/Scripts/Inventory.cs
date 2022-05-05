using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Truck truck = collision.GetComponent<Truck>();
        if (truck)
        {
            truck.AddAllItemsToMission();
            truck.ClearTruck();
        }
    }
}
