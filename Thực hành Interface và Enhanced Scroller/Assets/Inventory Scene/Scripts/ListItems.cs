using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ListItems : MonoBehaviour
{
    public List<Item> listItems;

    [SerializeField] private Item prefab;
    [SerializeField] public ListCarry listCarry;
    [SerializeField] private GameObject popup;
    [SerializeField] private Controller _controller;

    private string pathResourceItem;
    private Sprite[] items;


    private void Start()
    {
        listItems = new List<Item>();
        LoadAllDefaultSprites();
        SetAllItemsToTheList();
        LoadFromDatabase();
    }

    void LoadAllDefaultSprites()
    {
        pathResourceItem = "Equipments";
        items = Resources.LoadAll<Sprite>(pathResourceItem);
    }

    private void SetAllItemsToTheList()
    {
        // set items
        foreach (Sprite item in items)
        {
            Item newItem = Instantiate(prefab, gameObject.transform);
            newItem.SetData(
                item.name,
                false,
                item.name.Substring(0, item.name.Length - 1),
                item,
                popup
            );
            listItems.Add(newItem);
        }
    }

    public void ReloadAllItems(Item itemIsChoosing)
    {
        foreach (Item item in listItems)
        {
            if (itemIsChoosing.isCarry == false)
            {
                if (item.name == itemIsChoosing.name)
                {
                    item.isCarry = false;
                    item.gameObject.SetActive(true);
                }

                if (item.name != itemIsChoosing.name && item.type == itemIsChoosing.type)
                {
                    item.isCarry = true;
                    item.gameObject.SetActive(false);
                }
            }
            else
            {
                if (item.name == itemIsChoosing.name)
                {
                    item.isCarry = true;
                    item.gameObject.SetActive(false);
                }

                if (item.name != itemIsChoosing.name && item.type == itemIsChoosing.type)
                {
                    item.isCarry = false;
                    item.gameObject.SetActive(true);
                }
            }
        }
    }


    public void ReloadAllItemsWhileUnEquip(Item itemIsChoosing)
    {
        foreach (Item item in listItems)
        {
            if (item.name == itemIsChoosing.name)
            {
                item.isCarry = false;
                item.gameObject.SetActive(true);
            }
        }
    }

    private void LoadFromDatabase()
    {
        foreach (Item item in listItems)
        {
            foreach (ItemData itemCarryData in _controller.db.items)
            {
                if (item.name == itemCarryData.name && itemCarryData.isCarry == true)
                {
                    item.isCarry = true;
                    item.gameObject.SetActive(false);
                }
            }
        }
    }
}