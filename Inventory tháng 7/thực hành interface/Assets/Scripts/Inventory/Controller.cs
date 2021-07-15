using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] internal ScrollerController _scrollerController;
    [SerializeField] internal CarriesItemController _carriesItemController;
    [SerializeField] internal Popup _popup;
    [SerializeField] internal PlayerInventory _database;


    public virtual void Awake()
    {
        LoadData();
        SetDatabase();
        _scrollerController.SetItemOnClickDelegate(ShowPopup);
        _carriesItemController.SetItemOnClickDelegate(ShowPopup);
        _popup.SetEquipMethod(Equip);
        _popup.SetUnEquipMethod(UnEquip);
    }    


    // ................................ DELEGATE METHODS ..................................
    public void SetDatabase()
    {
        _scrollerController.SetDatabase(_database);
        _carriesItemController.SetDatabase(_database);
    }

    public virtual void ShowPopup(Item item)
    {
        _popup.ShowItemIsChoosing(item);
    }

    public virtual void Equip(Item item)
    {
        _scrollerController.RemoveItem(item);
        _carriesItemController.AddItem(item);
        _database.AddItem(item);
        SaveData();
    }

    public virtual void UnEquip(Item item)
    {
        _scrollerController.AddItem(item);
        _carriesItemController.RemoveItem(item);
        _database.RemoveItem(item);
        SaveData();
    }

    public void LoadData()
    {
        _database = new PlayerInventory();
    }

    public void SaveData()
    {
        _database.Save();
    }
}