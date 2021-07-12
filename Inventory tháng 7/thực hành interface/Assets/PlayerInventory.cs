using System.Collections.Generic;
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

    public PlayerInventory()
    {
        data = JsonConvert.DeserializeObject<PlayerInventoryData>(PlayerPrefs.GetString("database"));

        if (data == null)
        {
            data = new PlayerInventoryData();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString("database", JsonConvert.SerializeObject(data));
    }

    public void AddItem(Item item)
    {
        if (ItemIsExistOnList(item, data.listItemsAreNotCarried))
        {
            data.listItemsAreNotCarried.Remove(item);
        }

        data.listItemsAreCarried.Add(item);
        Save();
    }

    public void RemoveItem(Item item)
    {
        if (ItemIsExistOnList(item, data.listItemsAreCarried))
        {
            data.listItemsAreCarried.Remove(item);
        }

        data.listItemsAreNotCarried.Add(item);
        Save();
    }


    private bool ItemIsExistOnList(Item item, List<Item> list)
    {
        Item itemIsChoosing = list.Find(e => e.itemName == item.itemName);
        if (itemIsChoosing == null) return false;
        return true;
    }
}