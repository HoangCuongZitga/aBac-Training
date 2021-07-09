using System;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log(item.itemName);
        _popup.ShowItemIsChoosing(item);
    }

    void Equip(Item item)
    {
        _scrollerController.RemoveItem(item);
        _carriesItemController.AddItem(item);
        SaveData();
    }

    void UnEquip(Item item)
    {
        _scrollerController.AddItem(item);
        _carriesItemController.RemoveItem(item);
    }

    void LoadData()
    {
        string data = PlayerPrefs.GetString("database");
        if (data == "")
        {
            _database = new PlayerInventory();
            return;
        }

        _database = JsonConvert.DeserializeObject<PlayerInventory>(data);
    }

    void SaveData()
    {
        // _database.data.listItemsAreCarried = _carriesItemController.GetItemIsCarried();
        // _database.data.listItemsAreNotCarried = _scrollerController.GetItemIsNotCarried();

        _database.Save();
    }
}