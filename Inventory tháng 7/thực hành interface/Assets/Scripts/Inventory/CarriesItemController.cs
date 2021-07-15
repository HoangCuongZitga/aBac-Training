using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class CarriesItemController : MonoBehaviour
{
    [SerializeField] internal ScrollerController _scrollerController;
    [SerializeField] private ItemView itemCarriedPrefab;
    internal List<Item> _data;
    internal List<ItemView> listItemView;
    internal List<Sprite> _sprites;
    internal PlayerInventory _database;
    internal Action<Item> ItemOnClickDelegate;


//....................................... PRIVATE METHODS .......................................
    private void Start()
    {
        _data = new List<Item>();
        listItemView = new List<ItemView>();
        LoadData();

        //
        // string xxxx = "3003";
        // Debug.Log(xxxx[xxxx.Length - 1].ToString() + 1);
    }

    void LoadData()
    {
        // load all item from resource
        LoadAllItemDefaults();

        // load data from database
        if (_database.data.listItemsAreCarried.Count != 0)
        {
            foreach (Item item in _database.data.listItemsAreCarried)
            {
                if (_data.Contains(item) == false)
                {
                    _data.Add(item);
                    ItemView itemView = listItemView.Find(e => e.GetData().itemType == item.itemType);
                    if (itemView != null)
                    {
                        itemView.SetData(item);
                        itemView.ShowButton();
                    }
                }
            }
        }
    }

    void LoadAllItemDefaults()
    {
        _sprites = Resources.LoadAll<Sprite>("Equipped").ToList();
        for (int i = 0; i < _sprites.Count; i++)
        {
            Item newItem = new Item()
            {
                itemID = i,
                itemName = _sprites[i].name,
                itemType = _sprites[i].name,
                isCarried = false,
                itemLevel = 0
            };
            // _data.Add(newItem);
            ItemView item = Instantiate(itemCarriedPrefab, transform);
            listItemView.Add(item);
            item.SetData(newItem);
            item.SetActionOnClick(ItemOnClickDelegate);
            item.HideButton();
        }
    }

//....................................... PUBLIC METHODS .......................................
    public  void SetItemOnClickDelegate(Action<Item> method)
    {
        ItemOnClickDelegate = method;
    }

    public virtual bool AddItem(Item item)
    {
        ItemView itemTypePos = listItemView.Find(e => e.GetData().itemType == item.itemType);
        //replace item if it's the same type
        if (itemTypePos.GetData().itemName.Length > 3) _scrollerController.AddItem(itemTypePos.GetData());
        itemTypePos.SetData(item);
        itemTypePos.ShowButton();
        return true;
    }

    // ItemView Mode(Item item, string mode)
    // {
    //     if (CountTheItemIsCarried() == 3) return null;
    //
    //     if (mode == "fuse")
    //     {
    //         ItemView itemTypePos = listItemView.Find(e => e.GetData().itemName.Length == 3);
    //         if (itemTypePos != null) return itemTypePos;
    //         return listItemView[0];
    //     }
    //
    //     if (mode == "popup")
    //     {
    //         ItemView itemTypePos = listItemView.Find(e => e.GetData().itemType == item.itemType);
    //         return itemTypePos;
    //     }
    //
    //     return null;
    // }

    int CountTheItemIsCarried()
    {
        var xx = listItemView.Where(e => e.GetData().itemName.Length > 3).ToList();

        return xx.Count;
    }

    public void RemoveItem(Item item)
    {
        ItemView xxx = listItemView.Find(e => e.GetData().itemID == item.itemID);
        if (xxx == null) return;
        Item newItem = new Item()
        {
            itemID = 0,
            itemName = item.itemType,
            itemType = item.itemType,
            isCarried = false,
            itemLevel = 0
        };
        xxx.SetData(newItem);
        xxx.HideButton();
    }

    public void RemoveListItems(List<Item> listitem)
    {
        Item newItem = new Item()
        {
            itemID = 0,
            itemName = listitem[0].itemType,
            itemType = listitem[0].itemType,
            isCarried = false,
            itemLevel = 0
        };
        listItemView.ForEach(e =>
        {
            e.SetData(newItem);
            e.HideButton();
        });
    }

    public void SetDatabase(PlayerInventory database)
    {
        this._database = database;
    }

    public List<Item> GetItemIsCarried()
    {
        List<Item> itemsAreCarried = new List<Item>();

        listItemView.ForEach(e =>
        {
            if (e.GetData().isCarried == true) itemsAreCarried.Add(e.GetData());
        });
        return itemsAreCarried;
    }
}