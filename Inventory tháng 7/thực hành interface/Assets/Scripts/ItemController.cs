using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public List<Item> listItem;
    [SerializeField] Popup popup;
    public Dictionary<string, Sprite> defaultSpirite;

    private void Awake()
    {
        defaultSpirite = new Dictionary<string, Sprite>();
        foreach (Item item in listItem)
        {
            string nameitem = item.GetComponent<Image>().sprite.name;
            defaultSpirite.Add(nameitem, item.GetComponent<Image>().sprite);
            ItemData data = new ItemData()
            {
                nameItem = nameitem,
                isEquipped = false,
                itemType = nameitem
            };
            item.SetPopup(popup);
            item.SetData(data);
        }
    }
}