using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public Database dataBase = new Database();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        LoadData();
    }


    [Serializable]
    public class Database
    {
        public List<CardData> listData; //= new List<CardData>();
    }

    [Serializable]
    public class CardData
    {
        public int ID;
        public bool isBought;
    }


    public void SaveData(int id, bool isbought)
    {
        CardData data = new CardData {ID = id, isBought = isbought};
        if (dataBase.listData.Contains(data) == false)
        {
            dataBase.listData.Add(data);
        }
        else
        {
            dataBase.listData.Find(e => e.ID == data.ID).isBought = data.isBought;
        }


        string json = JsonUtility.ToJson(dataBase);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            dataBase = JsonUtility.FromJson<Database>(json);
        }
    }

    public CardData LoadCardData(int id)
    {
        CardData card = dataBase.listData.Find(e => e.ID == id);
        return card;
    }
}

//.............................................................................. MODEL ...........................................................
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    public int id;
    public bool isBought = false;

    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject darkPanel;
    [SerializeField] private Image imageOfItem;

    private void Start()
    {
        // add event for buy button
        buyButton.onClick.AddListener(BuyClickedEffect);
    }

    void BuyClickedEffect()
    {
        darkPanel.SetActive(true);
        isBought = true;
        DataManager.instance.SaveData(id, isBought);
    }

    public void SetSprite(int id)
    {
        this.id = id; //update id for item
        imageOfItem.sprite = Resources.Load("ImageOfItems/" + id, typeof(Sprite)) as Sprite;
        DataManager.CardData card = DataManager.instance.LoadCardData(this.id);
        
        if (card != null)
        {
            this.isBought = card.isBought;
            if (this.isBought == true) darkPanel.SetActive(true);
        }
    }
}


