using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarriesItemController : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private ScrollerController _scrollerController;
    [SerializeField] private Popup _popup;
    [SerializeField] private ItemView itemCarriedPrefab;
    private List<Item> _data;
    private List<ItemView> listItemView;
    private List<Sprite> _sprites;

    private void Start()
    {
        _data = new List<Item>();
        listItemView = new List<ItemView>();
        LoadData();
    }

    void LoadData()
    {
        // load data from database
        if (_dataManager.GetDatabase() != null)
        {
            _data = _dataManager.GetDatabase();
            return;
        }

        // load all item from resource
        _sprites = Resources.LoadAll<Sprite>("Equipped").ToList();

        for (int i = 0; i < _sprites.Count; i++)
        {
            Item newItem = new Item()
            {
                itemID = i,
                itemName = _sprites[i].name,
                itemType = _sprites[i].name,
                isCarried = false
            };
            _data.Add(newItem);
            ItemView item = Instantiate(itemCarriedPrefab, transform);
            listItemView.Add(item);
            item.SetData(newItem);
            item.SetActionOnClick(ItemOnClick);
        }
    }
    void ItemOnClick(Item item)
    {
        Debug.Log(item.itemName);
        _popup.ShowItemIsChoosing(item);
    }
    public void AddItem(Item item)
    {
        Item itemPos = _data.Find(e => e.itemType == item.itemType);
        ItemView xxx = listItemView.Find(e => e.GetData().itemType == itemPos.itemType);
        if(xxx.GetData().itemName.Any(c=>char.IsDigit(c))) _scrollerController.AddItem(xxx.GetData());
        xxx.SetData(item);
        
        Debug.Log(item.isCarried);
    } public void RemoveItem(Item item)
    {
         Item itemPos = _data.Find(e => e.itemType == item.itemType);
         ItemView xxx = listItemView.Find(e => e.GetData().itemType == itemPos.itemType);
         Item newItem = new Item()
         {
             itemID = 0,
             itemName = itemPos.itemType,
             itemType = itemPos.itemType,
             isCarried = false
         };
          _scrollerController.AddItem(xxx.GetData());
         xxx.SetData(newItem);
        
        Debug.Log(item.isCarried);
    }
}