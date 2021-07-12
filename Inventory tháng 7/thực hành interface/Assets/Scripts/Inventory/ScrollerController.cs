using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using Newtonsoft.Json;

public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] internal EnhancedScroller myScroller;
    [SerializeField] internal ItemView cellViewPrefab;
    private List<Item> _data;
    private List<Sprite> _sprites;
    [SerializeField] private PlayerInventory _database;
    internal Action<Item> ItemOnClickDelegate;


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
            foreach (Item item in _database.data.listItemsAreNotCarried)
            {
             _data.Add(item);
            }
            myScroller.ReloadData();
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