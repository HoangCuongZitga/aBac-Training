using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class Database
{
    public InventoryData data;
    private List<Sprite> _sprites;

    public Database()
    {
        _sprites = Resources.LoadAll<Sprite>("Item_Prototype2").ToList();
        data = new InventoryData();

        string database = PlayerPrefs.GetString("database");
        if (database != "") data = JsonConvert.DeserializeObject<InventoryData>(PlayerPrefs.GetString("database"));
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
                    ID = itemCount,
                    name = _sprites[i].name,
                    type = Int32.Parse(_sprites[i].name.Substring(0, 3)),
                    rarity = Int32.Parse(_sprites[i].name[3].ToString()),
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
}

public class InventoryData
{
    public List<Item> listItemsAreCarried;
    public List<Item> listItemsAreNotCarried;
}