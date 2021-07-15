using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControllerUpgrade : Controller
{
    [SerializeField] private UpgradeBtn _upgradeBtn;

    public override void Awake()
    {
        base.Awake();
        _scrollerController.SetItemOnClickDelegate(Equip);
        _carriesItemController.SetItemOnClickDelegate(UnEquip);
        _upgradeBtn.SetUpgradeBtnOnClick(CreateNewItem);
    }


    void CreateNewItem(List<Item> listItem)
    {
        _carriesItemController.RemoveListItems(listItem);
        _database.RemoveListItems(listItem);
        Item itemUpgraded = _upgradeBtn.CreateUpgradeItem(listItem);
        _database.AddItem(itemUpgraded);
        UnEquip(itemUpgraded);
        _upgradeBtn.TurnOnUpgradeCondition();
    }

    public override void Equip(Item item)
    {
        //  base.Equip(item);
        _upgradeBtn.AddItemForUpgrading(item);

        if (_carriesItemController.AddItem(item))
        {
            _scrollerController.RemoveItem(item);
            _database.AddItem(item);
            SaveData();
        }
    }


    public override void UnEquip(Item item)
    {
        // base.UnEquip(item);
        _scrollerController.AddItem(item);
        _carriesItemController.RemoveItem(item);
        _database.RemoveItem(item);
        
        _upgradeBtn.RemoveItemForUpgrading(item);
        SaveData();
    }
}