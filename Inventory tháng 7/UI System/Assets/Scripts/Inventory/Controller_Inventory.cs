using System.Collections;
using System.Collections.Generic;
using EnhancedScrollerDemos.GridSimulation;
using UnityEngine;

public class Controller_Inventory : MonoBehaviour
{
    internal Database _database;
    [SerializeField] private BoxBlueItems _boxBlueItems;
    [SerializeField] private ScrollerController_Inventory _scrollerControllerInventory;

   public virtual void Awake()
    {
        _database = new Database();
        _scrollerControllerInventory.SetDatabase(_database);
        _boxBlueItems.SetDatabase(_database);
        _scrollerControllerInventory.SetItemOnClickDelegate(Equip);
        _boxBlueItems.SetItemOnClickDelegate(UnEquip);
    }

   public virtual void Equip(Item item)
    {
        Item fakeItem = _boxBlueItems.listItemCarrying.Find(e => e.GetData().type == item.type).GetData();
        if (fakeItem.name.Length > 3)
        {
            _scrollerControllerInventory.AddItem(fakeItem);
            _database.RemoveItem(fakeItem);
        }

        _boxBlueItems.AddItem(item);
        _scrollerControllerInventory.RemoveItem(item);
        _database.AddItem(item);
        _database.Save();
    }

   public virtual  void UnEquip(Item item)
    {
        _boxBlueItems.RemoveItem(item);
        _scrollerControllerInventory.AddItem(item);
        _database.RemoveItem(item);
        _database.Save();
    }
}