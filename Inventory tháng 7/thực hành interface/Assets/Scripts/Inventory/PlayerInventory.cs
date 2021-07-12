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
        data.listItemsAreCarried = new List<Item>();
        data.listItemsAreNotCarried = new List<Item>();
        for (int i = 0; i < _sprites.Count; i++)
        {
            Item newItem = new Item()
            {
                itemID = i,
                itemName = _sprites[i].name,
                itemType = _sprites[i].name.Substring(0, _sprites[i].name.Length - 1),
                itemLevel = 1,
                isCarried = false
            };
            data.listItemsAreNotCarried.Add(newItem);
        }

        Debug.Log(data.listItemsAreNotCarried.Count);
    }

    public void Save()
    {
        PlayerPrefs.SetString("database", JsonConvert.SerializeObject(data));
    }

    public void AddItem(Item item)
    {
        data.listItemsAreCarried.Add(item);
        if (data.listItemsAreNotCarried.Contains(item))
            data.listItemsAreNotCarried.Remove(item);
        Save();
    }

    public void RemoveItem(Item item)
    {
        data.listItemsAreNotCarried.Add(item);
        if (data.listItemsAreCarried.Contains(item))
            data.listItemsAreCarried.Remove(item);
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