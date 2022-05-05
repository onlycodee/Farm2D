using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropUI : MonoBehaviour
{
    Image cropImage;
    // Start is called before the first frame update
    void Awake()
    {
        cropImage = GetComponent<Image>();    
    }

    public void SetImage(Sprite sprite)
    {
        cropImage.sprite = sprite;
    }
}
