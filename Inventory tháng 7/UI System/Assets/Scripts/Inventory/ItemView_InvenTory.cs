using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView_InvenTory : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemRarity;
    [SerializeField] private Text itemID;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button itemButton;
    private Item itemData;
    private Action<Item> onClick;

    private void Start()
    {
        itemButton.onClick.AddListener(() => { onClick.Invoke(itemData); });
    }


    //...................................PUBLIC METHODS ...............................
    public void SetData(Item item)
    {
        if (container != null) container.SetActive(item != null);
        if (item != null)
        {
            itemData = item;
            itemID.text = "ID " + item.ID;
            itemName.text = item.name;
            itemRarity.text = "Rarity " + item.rarity;
            //  change type if ...
            if (item.name.Length > 3) itemImage.sprite = Resources.Load<Sprite>("Item_Prototype2/" + item.name);
            else itemImage.sprite = Resources.Load<Sprite>("BlueHoldItem/" + item.type);
        }
    }

    public void SetActionOnClick(Action<Item> method)
    {
        onClick = method;
    }

    public Item GetData()
    {
        return itemData;
    }

    public void HideButton()
    {
        itemButton.enabled = false;
    }

    public void ShowButton()
    {
        itemButton.enabled = true;
    }
}