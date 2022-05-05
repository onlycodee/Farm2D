using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemUI : MonoBehaviour
{
    [SerializeField] Text txtNumber;
    [SerializeField] Image finishedImage;
    Image imageComp;
    ItemType type;


    private void Awake()
    {
        imageComp = GetComponent<Image>();
        finishedImage.gameObject.SetActive(false);
    }

    public void SetData(Item item)
    {
        imageComp.sprite = item.icon;
        txtNumber.text = item.number.ToString();
        type = item.type;
    }

    public void Finished()
    {
        if (!finishedImage.gameObject.activeInHierarchy)
        {
            txtNumber.gameObject.SetActive(false);
            finishedImage.gameObject.SetActive(true);
        }
    }

    public void DisplayFinishedIcon()
    {
        finishedImage.gameObject.SetActive(true);
    }

    public void SetNumber(int number)
    {
        txtNumber.text = number.ToString();
    }
}
