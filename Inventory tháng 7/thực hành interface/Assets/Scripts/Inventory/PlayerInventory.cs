using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerInventoryData
{
    public List<Item> listItemsAreCarried;
    public List<Item> listItemsAreNotCarried;
}

public class PlayerInventory
{
    public PlayerInventoryData data;
    private List<Sprite> _sprites;

    public PlayerInventory()
    {
        _sprites = Resources.LoadAll<Sprite>("Item_Prototype2").ToList();
        data = new PlayerInventoryData();

        string database = PlayerPrefs.GetString("database");
        if (database != "")
        {
            data = JsonConvert.DeserializeObject<PlayerInventoryData>(PlayerPrefs.GetString("database"));
        }
        else
        {
            LoadAllItemDefaults();
        }
    }

    void LoadAllItemDefaults()
    {
        int itemCount = 0;
        data.listItemsAreCarried = new List<Item>();
        data.listItemsAreNotCarried = new List<Item>();
        for (int i = 0; i < _sprites.Count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Item newItem = new Item()
                {
                    itemID = itemCount,
                    itemName = _sprites[i].name,
                    itemType = _sprites[i].name.Substring(0, 3),
                    itemLevel = Int32.Parse( _sprites[i].name[3].ToString()),
                    isCarried = false
                };
                data.listItemsAreNotCarried.Add(newItem);
                itemCount += 1;
            }
        }
    }


    public void Save()
    {
        PlayerPrefs.SetString("database", JsonConvert.SerializeObject(data));
    }

    public void AddItem(Item item)
    {
        // if having a item with same type carried. We must unequip it
        Item itemSameType = data.listItemsAreCarried.Find(e => e.itemType == item.itemType);
        if (itemSameType != null && data.listItemsAreCarried.Contains(itemSameType))
        {
            data.listItemsAreCarried.Remove(itemSameType);
            if (data.listItemsAreNotCarried.Find(e => e.itemName == itemSameType.itemName) == null)
            {
                data.listItemsAreNotCarried.Add(itemSameType);
            }
        }

        data.listItemsAreCarried.Add(item);
        data.listItemsAreNotCarried.Remove(item);
        Save();
    }

    public void RemoveItem(Item item)
    {
        data.listItemsAreCarried.Remove(item);
        data.listItemsAreNotCarried.Add(item);
        Save();
    }

    public void RemoveListItems(List<Item> listitem)
    {
        foreach (Item item in listitem)
        {
            if (data.listItemsAreCarried.Contains(item))
            {
                data.listItemsAreCarried.Remove(item);
            }
        }

        Save();
    }

    bool ItemExistsOnList(Item item, List<Item> list)
    {
        Item finder = list.Find(e => e.itemName == item.itemName);
        if (finder == null) return false;
        return true;
    }

    Item ItemSameTypeOfThisExistsOnList(Item item, List<Item> list)
    {
        Item finder = list.Find(e => e.itemType == item.itemType);
        if (finder == null) return null;
        return finder;
    }
}