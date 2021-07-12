using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;

public class ItemView : EnhancedScrollerCellView
{
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemID;
    [SerializeField] private Text itemLevel;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button itemButton;
    private Item itemData;

    private Action<Item> onClick;

    private void Start()
    {
        itemButton.onClick.AddListener(() => { onClick.Invoke(itemData); });
    }

    public void SetActionOnClick(Action<Item> method)
    {
        onClick = method;
    }

    public void SetData(Item item)
    {
        itemData = item;
        itemName.text = item.itemName;
        itemID.text = "ID " + item.itemID;
        itemLevel.text = "Level " + item.itemLevel;
        //  change type if ...
        if (item.itemName.Any(c => char.IsDigit(c))) itemImage.sprite = Resources.Load<Sprite>("Item_Prototype2/" + item.itemName);
        else itemImage.sprite = Resources.Load<Sprite>("Equipped/" + item.itemName);
    }

    public Item GetData()
    {
        return itemData;
    }

    public void HideButton()
    {
        itemButton.enabled = false;
    }
}