using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;

public class ItemCarry : EnhancedScrollerCellView
{
    [SerializeField] Text itemName;
    [SerializeField] Image itemImage;


    public void SetData(ItemCarryData data)
    {
        this.itemName.text = data.itemName;
        this.itemImage.sprite = data.itemImage;
    }
}