using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private ListItems listItems;
    [SerializeField] private ListCarry listCarry;

    private void Start()
    {
        // Debug.Log(listItems.listItems.Count);
        SetEventForeachItem();
    }

    void SetEventForeachItem()
    {
        foreach (Item item in listItems.listItems)
        {
            item.button.onClick.AddListener(() => { _popup.ShowPopup(item); });
        }
    }
}