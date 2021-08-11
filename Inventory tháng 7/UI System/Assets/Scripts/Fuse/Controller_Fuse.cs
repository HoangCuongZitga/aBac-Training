using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Fuse : Controller_Inventory
{
    [SerializeField] private BoxBlueItemsChoosing _boxBlueItemsChoosing;
    [SerializeField] private Scroller_Fuse _scrollerFuse;
    public override void Awake()
    {
        _database = new Database();
        _boxBlueItemsChoosing.SetDatabase(_database);
        _scrollerFuse.SetDatabase(_database);
   //     _scrollerFuse.SetItemOnClickDelegate(Equip);
   //     _boxBlueItemsChoosing.SetItemOnClickDelegate(UnEquip);
    }

    public override void Equip(Item item)
    {
    }

    public override void UnEquip(Item item)
    {
    }
}
