using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnhancedUI.EnhancedScroller;

public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private DataManager dataManager;
    [SerializeField] private EnhancedScroller myScroller;
    [SerializeField] private ItemView cellViewPrefab;
    private List<Item> _data;
    private List<Sprite> _sprites;

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
        // load data from database
        if (dataManager.GetDatabase() != null)
        {
            _data = dataManager.GetDatabase();
            return;
        }

        // load all item from resource
        _sprites = Resources.LoadAll<Sprite>("Item_Prototype2").ToList();

        for (int i = 0; i < _sprites.Count; i++)
        {
            Item newItem = new Item()
            {
                itemID = i,
                itemName = _sprites[i].name,
                itemType = _sprites[i].name.Substring(0, _sprites[i].name.Length - 1)
            };
            _data.Add(newItem);
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
        myScroller.ReloadData();
    }

    public void AddItem(Item item)
    {
        item.isCarried = false;
        _data.Add(item);
        myScroller.ReloadData();
        myScroller.JumpToDataIndex(_data.IndexOf(item));
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
        // cellView.SetActionOnClick(ItemOnClick);
        cellView.SetActionOnClick(ItemOnClickDelegate);
        return cellView;
    }
}