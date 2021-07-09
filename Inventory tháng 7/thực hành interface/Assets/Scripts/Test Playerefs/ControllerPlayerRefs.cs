using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPlayerRefs : MonoBehaviour
{
    [SerializeField] private Button save;
    [SerializeField] private Button load;


    private void Start()
    {
        save.onClick.AddListener(Save);
        load.onClick.AddListener(Load);
    }


    void Save()
    {
        string str = "asdasdasd";
        int INT = 12;
        List<int> listInt = new List<int>() {1, 2, 3, 4, 5};
        PlayerPrefs.SetString("string", str);
        PlayerPrefs.SetInt("int", INT);
        PlayerPrefs.SetInt("int", INT);
        PlayerPrefs.SetString("listInt", JsonConvert.SerializeObject(listInt));
        PlayerPrefs.SetString("class", JsonConvert.SerializeObject(new test()));
        // PlayerPrefs.Save();

        Debug.Log("Saved!");
    }

    void Load()
    {
        string data = PlayerPrefs.GetString("class");
        test listInt = new test();
        listInt = JsonConvert.DeserializeObject<test>(data);
        listInt.dictionary.Add(1,"one");
        PlayerPrefs.SetString("class", JsonConvert.SerializeObject(listInt));
         data = PlayerPrefs.GetString("class");
        Debug.Log(data);
    }
}

public class test
{
    public string str = "dasdas";
    public int intVariable = 111;

    List<int> listInt = new List<int>() {13, 123, 123, 123, 12};
    public Dictionary<int, string> dictionary = new Dictionary<int, string>();

   

}