using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControllerUpgrade : Controller
{
    [SerializeField] private UpgradeBtn _upgradeBtn;

    private void Start()
    {
        _scrollerController.SetItemOnClickDelegate(Equip);
        _carriesItemController.SetItemOnClickDelegate(UnEquip);
        _upgradeBtn.SetUpgradeBtnOnClick(CreateNewItem);
    }

    void CreateNewItem(List<Item> listItem)
    {
        _carriesItemController.RemoveListItems(listItem);
        _database.RemoveListItems(listItem);
        Item itemUpgraded = _upgradeBtn.UpgradeItem(listItem);
        _database.AddItem(itemUpgraded);
        UnEquip(itemUpgraded);
    }

    public override void Equip(Item item)
    {
        base.Equip(item);
        _upgradeBtn.AddItemForUpgrading(item);
    }


    public override void UnEquip(Item item)
    {
        base.UnEquip(item);
        _upgradeBtn.RemoveItemForUpgrading(item);
    }
}