using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

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
    
    public void AddItem(Item newItem)
    {
        data.listItemsAreNotCarried.Add(newItem);
        Save();
    }
}

public class PlayerInventoryData
{
    public List<Item> listItemsAreCarried = null;
    public List<Item> listItemsAreNotCarried = null;
}