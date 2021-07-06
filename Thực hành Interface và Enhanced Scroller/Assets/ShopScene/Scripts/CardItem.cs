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

