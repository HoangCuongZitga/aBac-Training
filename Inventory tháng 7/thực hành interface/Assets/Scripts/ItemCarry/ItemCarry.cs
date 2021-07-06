using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCarry 
{
    [SerializeField] Image itemImage;
    

    public void SetData(ItemData data)
    {
        this.itemImage.sprite = data.itemImage;
    }
}
