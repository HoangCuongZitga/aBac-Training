using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Database
{
    public List<ItemData> items;

    public void SaveData(List<ItemData> data)
    {
        items = data;
    }
}

[Serializable]
public class ItemData
{
    public string name;
    public bool isCarry;
    public string type;

    public ItemData(string n, bool i, string ty)
    {
        this.name = n;
        this.isCarry = i;
        this.type = ty;
    }
}