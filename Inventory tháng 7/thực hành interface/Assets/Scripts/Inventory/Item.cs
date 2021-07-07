using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;

public class Item : EnhancedScrollerCellView, ItemInterface
{
    public Text itemName;
    public string itemType;
    public Image itemImage;
    public ItemData itemData;

    [SerializeField] Button itemButton;
    private Popup popup;

    public void SetPopup(Popup popupObj)
    {
        popup = popupObj;
    }

    public void SetData(ItemData data)
    {
        itemData = data;
        if (data.isEquipped == false) itemName.text = data.itemType;
        else itemName.text = data.nameItem;
        itemType = data.itemType;
    }

    public void SetSprite(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }

    private void Start()
    {
        itemButton.onClick.AddListener(() => { OnClicked(); });
    }


    public void OnClicked()
    {
        popup.ShowPopup(this);
        Debug.Log($"this is {itemData.nameItem}");
    }
}