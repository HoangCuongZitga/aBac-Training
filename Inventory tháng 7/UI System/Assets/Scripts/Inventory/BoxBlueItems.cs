using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxBlueItems : MonoBehaviour
{
    [SerializeField] public List<ItemView_InvenTory> listItemCarrying;
    private List<Sprite> _sprites;
    private Action<Item> onClick;
    private List<Item> _data;
    private Database _database;

    private void Start()
    {
        listItemCarrying.ForEach(e => { e.SetActionOnClick(onClick); });
        LoadBlueBoxes();
    }

    private void LoadBlueBoxes()
    {
        _sprites = Resources.LoadAll<Sprite>("BlueHoldItem").ToList();

        for (int i = 0; i < listItemCarrying.Count; i++)
        {
            Item itemFake = new Item()
            {
                ID = i,
                name = _sprites[i].name,
                type = Int32.Parse(_sprites[i].name),
                rarity = 1,
                isCarried = false
            };

            listItemCarrying[i].SetData(itemFake);
            listItemCarrying[i].HideButton();
        }

        for (int i = 0; i < _database.data.listItemsAreCarried.Count; i++)
        {
            listItemCarrying.ForEach(e =>
            {
                if (e.GetData().type == _database.data.listItemsAreCarried[i].type)
                {
                    e.SetData(_database.data.listItemsAreCarried[i]);
                    e.ShowButton();
                }
            });
        }
    }

    public void SetItemOnClickDelegate(Action<Item> method)
    {
        onClick = method;
    }

    public void SetDatabase(Database database)
    {
        _database = database;
    }

    public void AddItem(Item item)
    {
        ItemView_InvenTory itemView = listItemCarrying.Find(e => e.GetData().type == item.type);
        itemView.SetData(item);
        itemView.ShowButton();
    }

    public void RemoveItem(Item item)
    {
        ItemView_InvenTory xxx = listItemCarrying.Find(e => e.GetData().ID == item.ID);
        if (xxx == null) return;
        Item newItem = new Item()
        {
            ID = 0,
            name = item.type.ToString(),
            type = item.type,
            isCarried = false,
            rarity = 0
        };
        xxx.SetData(newItem);
        xxx.HideButton();
    }
}