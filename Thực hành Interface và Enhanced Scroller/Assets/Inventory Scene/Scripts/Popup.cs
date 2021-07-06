using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private Image imageChoosing;
    [SerializeField] private Button cancel;
    [SerializeField] private Button equip;
    [SerializeField] private Button unEquip;

    [SerializeField] private GameObject popupGameObj;
    [SerializeField] private Controller _controller;

    // 2 list contain item on UI
    [SerializeField] private ListItems listItems;
    [SerializeField] private ListCarry listCarry;


    private Item itemIsChoosing;
    private List<ItemData> data;

    private void Start()
    {
        data = _controller.db.items;

        cancel.onClick.AddListener(CancelClick);
        equip.onClick.AddListener(EquipClick);
        unEquip.onClick.AddListener(UnEquipClick);
    }

    public void SetSprite(Image newImage)
    {
        imageChoosing.sprite = newImage.sprite;
    }

    public void ShowPopup(Item item)
    {
        // check if item is "Blue Box" - Item Carry Box
        if (item.name.Any(c => char.IsDigit(c)) == false) return;
        if (item.isCarry == true) UnEquipMode();
        else EquipMode();
        SetSprite(item.image);
        itemIsChoosing = item;

        popupGameObj.SetActive(true);
        cancel.gameObject.SetActive(true);
    }

    void EquipMode()
    {
        equip.gameObject.SetActive(true);
        unEquip.gameObject.SetActive(false);
    }

    void UnEquipMode()
    {
        equip.gameObject.SetActive(false);
        unEquip.gameObject.SetActive(true);
    }

    void CancelClick()
    {
        popupGameObj.SetActive(false);
    }

    void EquipClick()
    {
        itemIsChoosing.isCarry = true;
        ItemData checkData = data.Find(e => itemIsChoosing.type == e.type);
        if (checkData == null)
        {
            data.Add(new ItemData(itemIsChoosing.name, itemIsChoosing.isCarry, itemIsChoosing.type));
        }
        else
        {
            data.Remove(checkData);
            data.Add(new ItemData(itemIsChoosing.name, itemIsChoosing.isCarry, itemIsChoosing.type));
        }

        listItems.ReloadAllItems(itemIsChoosing);
        listCarry.ReloadAllItems(itemIsChoosing);
        SaveToDatabase();
    }

    void UnEquipClick()
    {
        itemIsChoosing.isCarry = false;
        ItemData checkData = data.Find(e => itemIsChoosing.name == e.name);
        data.Remove(checkData);
        listItems.ReloadAllItemsWhileUnEquip(itemIsChoosing);
        listCarry.ReloadAllItems(itemIsChoosing);
        SaveToDatabase();
    }


    void SaveToDatabase()
    {
        CancelClick();
        _controller.db.SaveData(data);
        _controller.SaveDataToAFile(_controller.db);
    }
}