using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using Newtonsoft.Json;

public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private EnhancedScroller myScroller;
    [SerializeField] private ItemView cellViewPrefab;
    private List<Item> _data;
    private List<Sprite> _sprites;
    [SerializeField] private PlayerInventory _database;
    private Action<Item> ItemOnClickDelegate;


    // ................................ PRIVATE METHODS ......................................
    void Start()
    {
        _data = new List<Item>();
        LoadData();
        myScroller.Delegate = this;
        myScroller.ReloadData();
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
                _data = _data.Where(e => e.itemName != item.itemName).ToList();
            }
            myScroller.ReloadData();
        }
    }

    void LoadAllItemDefaults()
    {
        _sprites = Resources.LoadAll<Sprite>("Item_Prototype2").ToList();

        for (int i = 0; i < _sprites.Count; i++)
        {
            Item newItem = new Item()
            {
                itemID = i,
                itemName = _sprites[i].name,
                itemType = _sprites[i].name.Substring(0, _sprites[i].name.Length - 1),
                itemLevel = 0
            };
            _data.Add(newItem);
        }
    }

    IEnumerator ReloadData(Item item)
    {
        yield return new WaitForEndOfFrame();
        myScroller.ReloadData();
        if (item != null)
        {
            myScroller.JumpToDataIndex(_data.IndexOf(item));
        }
    }

    // ................................ PUBLIC METHODS ......................................
    public void SetItemOnClickDelegate(Action<Item> method)
    {
        ItemOnClickDelegate = method;
    }


    public void RemoveItem(Item item)
    {
        item.isCarried = true;
        _data.Remove(item);
        StartCoroutine(ReloadData(item));
    }

    public void AddItem(Item item)
    {
        item.isCarried = false;
        _data.Add(item);
        StartCoroutine(ReloadData(item));
    }


    public void SetDatabase(PlayerInventory database)
    {
        this._database = database;
    }
    // ................................ DEFAULT METHODS ......................................

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        ItemView cellView = scroller.GetCellView(cellViewPrefab) as ItemView;
        cellView.SetData(_data[dataIndex]);
        cellView.SetActionOnClick(ItemOnClickDelegate);
        return cellView;
    }
}