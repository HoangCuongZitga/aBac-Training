using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ListCarry : MonoBehaviour
{
    [SerializeField] private Item prefab;

    [SerializeField] private ListItems listItems;
    [SerializeField] private GameObject popup;
    [SerializeField] private Controller _controller;
    private string pathResourceItem;
    private string pathResourceHolderItem;
    private Sprite[] items;
    private Sprite[] itemHolders;

    public List<Item> listHolders;

    private void Start()
    {
        listHolders = new List<Item>();
        LoadAllDefaultSprites();
        SetAllItemsToTheList();
        LoadFromDatabase();
    }

    void LoadAllDefaultSprites()
    {
        pathResourceHolderItem = "Equipped";
        itemHolders = Resources.LoadAll<Sprite>(pathResourceHolderItem);
        items = Resources.LoadAll<Sprite>("Equipments");
    }

    private void SetAllItemsToTheList()
    {
        //set item holders
        foreach (Sprite holder in itemHolders)
        {
            Item newHolder = Instantiate(prefab, gameObject.transform);
            newHolder.SetData(
                holder.name,
                false,
                holder.name,
                holder,
                popup
            );
            listHolders.Add(newHolder);
        }
    }


    public void ReloadAllItems(Item item)
    {
        foreach (Item itemCarry in listHolders)
        {
            if (itemCarry.type == item.type)
            {
                itemCarry.isCarry = item.isCarry;
                if (item.isCarry == false)
                {
                    itemCarry.name = "no name";
                    itemCarry.image.sprite = itemHolders.ToList().Find(e => e.name == item.type);
                }
                else
                {
                    itemCarry.name = item.name;
                    itemCarry.image.sprite = item.GetComponent<Image>().sprite;
                }
            }
        }
    }

    

    private void LoadFromDatabase()
    {
        foreach (Item item in listHolders)
        {
            foreach (ItemData itemCarryData in _controller.db.items)
            {
                if (item.type == itemCarryData.type && itemCarryData.isCarry == true)
                {
                    item.isCarry = itemCarryData.isCarry;
                    item.name = itemCarryData.name;
                    item.image.sprite = items.ToList().Find(e => e.name == itemCarryData.name);
                }
            }
        }
    }
}