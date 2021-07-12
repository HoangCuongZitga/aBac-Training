using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private ItemView _itemView;
    [SerializeField] private Button equipBtn;
    [SerializeField] private Button UnEquipBtn;

    private Item itemIsChoosing;
    private Action<Item> equipMothod;
    private Action<Item> unEquipMothod;

    // ............................ PRIVATE METHOD .........................................
    private void Start()
    {
        _itemView.HideButton(); // disable click on this itemView
        gameObject.SetActive(false); // hide popup
        equipBtn.onClick.AddListener(Equip);
        UnEquipBtn.onClick.AddListener(UnEquip);
    }

    private void Equip()
    {
        equipMothod.Invoke(itemIsChoosing);
        gameObject.SetActive(false);
    }

    private void UnEquip()
    {
        unEquipMothod.Invoke(itemIsChoosing);
        gameObject.SetActive(false);
    }

    void EquipMode()
    {
        equipBtn.gameObject.SetActive(true);
        UnEquipBtn.gameObject.SetActive(false);
    }

    void UnEquipMode()
    {
        equipBtn.gameObject.SetActive(false);
        UnEquipBtn.gameObject.SetActive(true);
    }

    // ............................ PUBLIC METHOD .........................................
    public void SetEquipMethod(Action<Item> method)
    {
        equipMothod = method;
    }

    public void SetUnEquipMethod(Action<Item> method)
    {
        unEquipMothod = method;
    }

    public void ShowItemIsChoosing(Item item)
    {
        // if item is a holding blue box : return
        if (item.itemName.Any(c => char.IsDigit(c)) == false) return;
        itemIsChoosing = item;
        _itemView.SetData(item);
        if (item.isCarried == false) EquipMode();
        else UnEquipMode();

        gameObject.SetActive(true);
    }
}