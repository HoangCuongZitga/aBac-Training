using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private CarriesItemController _carriesItemController;
    [SerializeField] private ScrollerController _scrollerController;

    private void Start()
    {
        _scrollerController.SetItemOnClickDelegate(ShowPopup);
        _carriesItemController.SetItemOnClickDelegate(ShowPopup);
        _popup.SetEquipMethod(Equip);
        _popup.SetUnEquipMethod(UnEquip);
    }


    // ................................ DELEGATE METHODS ..................................

    void ShowPopup(Item item)
    {
        Debug.Log(item.itemName);
        _popup.ShowItemIsChoosing(item);
    }

    void Equip(Item item)
    {
        _scrollerController.RemoveItem(item);
        _carriesItemController.AddItem(item);
    }

    void UnEquip(Item item)
    {
        _scrollerController.AddItem(item);
        _carriesItemController.RemoveItem(item);
    }
}