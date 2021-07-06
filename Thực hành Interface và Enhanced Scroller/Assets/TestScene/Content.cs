using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Content : MonoBehaviour
{
    [SerializeField] private int numberOfItem;
    public SubData itemPrefabs;

    public List<SubData> newListItem = new List<SubData>();

    void Start()
    {
        GenerateListItem();
    }

    void GenerateListItem()
    {
        // set parent foreach item to list and set number for them
        for (int i = 0; i < numberOfItem; i++)
        {
            SubData newItem = Instantiate(itemPrefabs, transform);
            newItem.SetNameItem($"item {i + 1}");
            newListItem.Add(newItem);
        }
    }
}