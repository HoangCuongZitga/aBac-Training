using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Controller : MonoBehaviour
{
    public Database db;

    void Awake()
    {
        db = new Database();
        db.items = new List<ItemData>();
        LoadData();
    }

    public void SaveDataToAFile(Database csdl)
    {
        string json = JsonUtility.ToJson(csdl);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            db = JsonUtility.FromJson<Database>(json);
        }
    }
}