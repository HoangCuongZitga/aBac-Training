using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private PlayerInventory database;

    private void Awake()
    {
        database = new PlayerInventory();
        LoadDataFromJson();
    }

    void LoadDataFromJson()
    {
        string path = Application.dataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            database = JsonUtility.FromJson<PlayerInventory>(json);
        }
    }

  
}