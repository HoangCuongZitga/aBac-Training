using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class CarriesItemController : MonoBehaviour
{
    [SerializeField] private ScrollerController _scrollerController;
    [SerializeField] private ItemView itemCarriedPrefab;
    private List<Item> _data;
    private List<ItemView> listItemView;
    private List<Sprite> _sprites;
    private PlayerInventory _database;
    private Action<Item> ItemOnClickDelegate;

//....................................... PRIVATE METHODS .......................................
    private void Start()
    {
        _data = new List<Item>();
        listItemView = new List<ItemView>();
        LoadData();
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
    public void SetItemOnClickDelegate(Action<Item> method)
    {
        ItemOnClickDelegate = method;
    }

    public void AddItem(Item item)
    {
        ItemView xxx = listItemView.Find(e => e.GetData().itemType == item.itemType);

        //replace item if it's the same type
        if (xxx.GetData().itemName.Any(c => char.IsDigit(c))) _scrollerController.AddItem(xxx.GetData());
        xxx.SetData(item);
        xxx.ShowButton();
    }

    public void RemoveItem(Item item)
    {
        ItemView xxx = listItemView.Find(e => e.GetData().itemType == item.itemType);
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
        listitem.ForEach(e => { RemoveItem(e); });
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