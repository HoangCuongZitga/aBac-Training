using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private ScrollerController _scrollerController;
    [SerializeField] private CarriesItemController _carriesItemController;

    [SerializeField] private Image itemChoosingImage;
    [SerializeField] private Text itemChoosingName;
    [SerializeField] private Text itemChoosingID;
    [SerializeField] private Button equipBtn;
    [SerializeField] private Button UnEquipBtn;

    private Item itemIsChoosing;


    private void Start()
    {
        gameObject.SetActive(false); // hide popup
        equipBtn.onClick.AddListener(Equip);
        UnEquipBtn.onClick.AddListener(UnEquip);
    }

    public void ShowItemIsChoosing(Item item)
    {
        itemIsChoosing = item;
        itemChoosingName.text = item.itemName;
        itemChoosingID.text = item.itemID.ToString();
        itemChoosingImage.sprite = Resources.Load<Sprite>("Item_Prototype2/" + item.itemName);
        if (item.isCarried == false) EquipMode();
        else UnEquipMode();
      
        gameObject.SetActive(true);
    }

    void Equip()
    {
        _scrollerController.RemoveItem(itemIsChoosing);
        _carriesItemController.AddItem(itemIsChoosing);
        gameObject.SetActive(false);
    }

    void UnEquip()
    {
        _scrollerController.AddItem(itemIsChoosing);
        _carriesItemController.RemoveItem(itemIsChoosing);
        gameObject.SetActive(false);
    }
    void EquipMode()
    {
        equipBtn.gameObject.SetActive(true);
        UnEquipBtn.gameObject.SetActive(false);
    }
    void UnEquipMode()
    {
        equipBtn.gameObject.SetActive(false);
        UnEquipBtn.gameObject.SetActive(true);
    }
}