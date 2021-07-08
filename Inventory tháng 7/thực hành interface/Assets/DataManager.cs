using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private Database database;

    private void Awake()
    {
        database = new Database();
        LoadDataFromJson();
    }

    void LoadDataFromJson()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            database = JsonUtility.FromJson<Database>(json);
        }
    }

    public List<Item> GetDatabase()
    {
        return database.listItem;
    }
}