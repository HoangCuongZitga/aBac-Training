using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradeBtn : MonoBehaviour
{
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private ItemView _itemUpgradePreview;
    private List<Item> listItemForUpgrading;
    private Color32 hideMode = new Color32(95, 95, 95, 255);
    private Color32 showMode = new Color32(255, 255, 255, 255);
    private Action<List<Item>> upgradeBtnOnClick;

    public void SetUpgradeBtnOnClick(Action<List<Item>> method)
    {
        upgradeBtnOnClick = method;
    }

    private void Start()
    {
        _itemUpgradePreview.HideButton();
        listItemForUpgrading = new List<Item>();
        TurnOnTurnOffUpgradeBtn();
        upgradeBtn.onClick.AddListener(() => { upgradeBtnOnClick.Invoke(listItemForUpgrading); });
    }

    // ................................... PRIVATE METHODS ....................................
    private void TurnOnTurnOffUpgradeBtn()
    {
        if (listItemForUpgrading.Count == 3)
        {
            upgradeBtn.enabled = true;
            upgradeBtn.GetComponent<Image>().color = showMode;
        }
        else
        {
            upgradeBtn.enabled = false;
            upgradeBtn.GetComponent<Image>().color = hideMode;
        }

//        Debug.Log(listItemForUpgrading.Count);
    }

    public Item UpgradeItem(List<Item> listItem)
    {
        int random = Random.Range(0, listItem.Count - 1);
        Item newItemUpgraded = listItem[random];

        int newLevel = 0;
        listItem.ForEach(e => { newLevel += e.itemLevel; });
        newItemUpgraded.itemLevel = newLevel;

        _itemUpgradePreview.SetData(newItemUpgraded);

        return newItemUpgraded;
    }

    // ................................... PUBLIC METHODS ....................................
    public void AddItemForUpgrading(Item item)
    {
        if (listItemForUpgrading.Count == 3) return;
        listItemForUpgrading.Add(item);
        TurnOnTurnOffUpgradeBtn();
    }


    public void RemoveItemForUpgrading(Item item)
    {
        if (listItemForUpgrading.Contains(item)) listItemForUpgrading.Remove(item);
        TurnOnTurnOffUpgradeBtn();
    }
}