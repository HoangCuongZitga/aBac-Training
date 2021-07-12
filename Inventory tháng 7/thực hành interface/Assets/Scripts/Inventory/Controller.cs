using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private ScrollerController _scrollerController;
    [SerializeField] private CarriesItemController _carriesItemController;
    [SerializeField] private PlayerInventory _database;


    private void Awake()
    {
        LoadData();
        SetDatabase();
        _scrollerController.SetItemOnClickDelegate(ShowPopup);
        _carriesItemController.SetItemOnClickDelegate(ShowPopup);
        _popup.SetEquipMethod(Equip);
        _popup.SetUnEquipMethod(UnEquip);
    }


    // ................................ DELEGATE METHODS ..................................
    void SetDatabase()
    {
        _scrollerController.SetDatabase(_database);
        _carriesItemController.SetDatabase(_database);
    }

    void ShowPopup(Item item)
    {
        _popup.ShowItemIsChoosing(item);
    }

    void Equip(Item item)
    {
        _scrollerController.RemoveItem(item);
        _carriesItemController.AddItem(item);
        _database.AddItem(item);
        SaveData();
    }

    void UnEquip(Item item)
    {
        _scrollerController.AddItem(item);
        _carriesItemController.RemoveItem(item);
        _database.RemoveItem(item);
        SaveData();
    }

    void LoadData()
    {
        string data = PlayerPrefs.GetString("database");
        _database = new PlayerInventory();
        Debug.Log(data);
    }

    void SaveData()
    {
        _database.Save();
    }
}