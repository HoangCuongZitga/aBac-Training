using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;

public class Item : EnhancedScrollerCellView
{
    [SerializeField] Text itemName;
    [SerializeField] Image itemImage;
    

    public void SetData(ItemData data)
    {
        this.itemName.text = data.nameItem;
        this.itemImage.sprite = data.itemImage;
    }
}