using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private Button equipButton;
    [SerializeField] private Button unEquipButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Image imagePresentation;
    [SerializeField] private ItemController itemController;
    [SerializeField] private ScrollerController scrollerController;

    private Item itemIsChoosing;

    private void Start()
    {
        gameObject.SetActive(false); // hide popup
        equipButton.onClick.AddListener(EquipClick);
        unEquipButton.onClick.AddListener(UnEquipClick);
        cancelButton.onClick.AddListener(cancelClick);
    }

    public void ShowPopup(Item item)
    {
        if (item.itemData.nameItem.Any(c => char.IsDigit(c)) == false) return;

        if (item.itemData.isEquipped == true) UnEquipMode();
        else EquipMode();

        itemIsChoosing = item;
        imagePresentation.sprite = item.itemImage.sprite; // set image for item is choosing
        gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }

    private void EquipMode()
    {
        unEquipButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(true);
    }

    private void UnEquipMode()
    {
        unEquipButton.gameObject.SetActive(true);
        equipButton.gameObject.SetActive(false);
    }

    private void EquipClick()
    {
        itemIsChoosing.itemData.isEquipped = true;
        Item itemCarry = itemController.listItem.Find(e => e.itemData.itemType == itemIsChoosing.itemData.itemType);
        //if (itemCarry.itemData.nameItem.Any(c => char.IsDigit(c)) == true)
        // {
        //   //   itemCarry.itemData.isEquipped = false;
        //   //   scrollerController.ReLoadData(itemCarry);
        //   Debug.Log("asdasdasjkdhasdkjahsdjk");
        //   // itemCarry.SetData(itemIsChoosing.itemData);
        //   // itemCarry.SetSprite(itemIsChoosing.itemImage.sprite);
        //   // scrollerController.ReLoadData(itemIsChoosing);
        // }
        // else
        // {
            itemCarry.SetData(itemIsChoosing.itemData);
            itemCarry.SetSprite(itemIsChoosing.itemImage.sprite);
            scrollerController.ReLoadData(itemIsChoosing);
      //  }
       
        cancelClick();
    }

    private void UnEquipClick()
    {
        itemIsChoosing.itemData.isEquipped = false;
        Item itemCarry = itemController.listItem.Find(e => e.itemData.itemType == itemIsChoosing.itemData.itemType);
        itemCarry.SetData(itemIsChoosing.itemData);
        itemCarry.SetSprite(itemController.defaultSpirite[itemIsChoosing.itemType]);
        scrollerController.ReLoadData(itemIsChoosing);
        cancelClick();
    }


    private void cancelClick()
    {
        gameObject.SetActive(false);
    }
}