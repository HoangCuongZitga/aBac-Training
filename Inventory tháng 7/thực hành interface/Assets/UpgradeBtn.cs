using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EnhancedScrollerDemos.SelectionDemo;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBtn : MonoBehaviour
{
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private ItemView _itemUpgradePreview;
    private List<Item> listItemForUpgrading;
    private Color32 hideMode = new Color32(95, 95, 95, 255);
    private Color32 showMode = new Color32(255, 255, 255, 255);
    private Action<List<Item>> upgradeBtnOnClick;

    public void SetUpgradeBtnOnClick(Action<List<Item>> method)
    {
        upgradeBtnOnClick = method;
    }

    private void Start()
    {
        _itemUpgradePreview.HideButton();
        listItemForUpgrading = new List<Item>();
        TurnOnUpgradeCondition();
        upgradeBtn.onClick.AddListener(() => { upgradeBtnOnClick.Invoke(listItemForUpgrading); });
    }

    // ................................... PRIVATE METHODS ....................................
    public void TurnOnUpgradeCondition()
    {
        if (listItemForUpgrading.Count == 3 && AllItemAreTheSameType(listItemForUpgrading) &&
            listItemForUpgrading[0].itemLevel <= 3)
        {
            upgradeBtn.enabled = true;
            upgradeBtn.GetComponent<Image>().color = showMode;
        }
        else
        {
            upgradeBtn.enabled = false;
            upgradeBtn.GetComponent<Image>().color = hideMode;
        }

//        Debug.Log(listItemForUpgrading.Count);
    }

    public Item CreateUpgradeItem(List<Item> listItem)
    {
        string fullName = listItem[0].itemName;
        string type = fullName.Substring(0, fullName.Length - 1);
        int level = Int32.Parse(fullName[fullName.Length - 1].ToString());

        Item newItem = new Item()
        {
            itemID = listItem[0].itemID,
            isCarried = false,
            itemName = type + (level + 1),
            itemLevel = level + 1,
            itemType = type,
        };
        _itemUpgradePreview.SetData(newItem);
        listItemForUpgrading.Clear();
        TurnOnUpgradeCondition();
        return newItem;
    }

    private bool AllItemAreTheSameType(List<Item> list)
    {
        if (list.Count == 0) return false;
        string type = list[0].itemName;
        foreach (Item item in list)
        {
            if (type != item.itemName) return false;
        }

        return true;
    }

    // ................................... PUBLIC METHODS ....................................
    public void AddItemForUpgrading(Item item)
    {
        if (listItemForUpgrading.Count == 3) return;
        listItemForUpgrading.Add(item);
        TurnOnUpgradeCondition();
    }


    public void RemoveItemForUpgrading(Item item)
    {
        if (listItemForUpgrading.Contains(item)) listItemForUpgrading.Remove(item);
        TurnOnUpgradeCondition();
    }
}