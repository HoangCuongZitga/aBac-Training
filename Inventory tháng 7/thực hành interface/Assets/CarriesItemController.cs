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
        if (_database.data.listItemsAreCarried != null)
        {
            foreach (Item item in _database.data.listItemsAreCarried)
            {
                if (_data.Contains(item) == false)
                {
                    _data.Add(item);
                    listItemView.Find(e => e.GetData().itemType == item.itemType).SetData(item);
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
                isCarried = false
            };
            _data.Add(newItem);
            ItemView item = Instantiate(itemCarriedPrefab, transform);
            listItemView.Add(item);
            item.SetData(newItem);
            item.SetActionOnClick(ItemOnClickDelegate);
        }
    }

//....................................... PUBLIC METHODS .......................................
    public void SetItemOnClickDelegate(Action<Item> method)
    {
        ItemOnClickDelegate = method;
    }

    public void AddItem(Item item)
    {
        Item itemPos = _data.Find(e => e.itemType == item.itemType);
        ItemView xxx = listItemView.Find(e => e.GetData().itemType == itemPos.itemType);

        //replace item if it's the same type
        if (xxx.GetData().itemName.Any(c => char.IsDigit(c))) _scrollerController.AddItem(xxx.GetData());

        xxx.SetData(item);
    }

    public void RemoveItem(Item item)
    {
        Item itemPos = _data.Find(e => e.itemType == item.itemType);
        ItemView xxx = listItemView.Find(e => e.GetData().itemType == itemPos.itemType);
        Item newItem = new Item()
        {
            itemID = 0,
            itemName = itemPos.itemType,
            itemType = itemPos.itemType,
            isCarried = false
        };
        xxx.SetData(newItem);
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