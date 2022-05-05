using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] Shop shop;
    [SerializeField] GameObject sellCropPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player.GetHoldingState() == HoldingState.TruckRope)
        {
            sellCropPanel.SetActive(true);
        } else
        {
            shop.SetMoneyInShop(Money.Instance.currentMoney);
            shop.gameObject.SetActive(true);
        }        
    }

    public void TurnOff()
    {
        Time.timeScale = 1;
        sellCropPanel.SetActive(false);
        shop.gameObject.SetActive(false);
    }
}
