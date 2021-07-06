using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour
{
    [SerializeField] private CardItem itemPrefab;
    
    private int[] itemChoosed = new int[] {0, 1, 2, 3, 4, 5};

    void Start()
    {
        GenerateShopItems();
    }

    void GenerateShopItems()
    {
        for (int i = 0; i < itemChoosed.Length; i++)
        {
            CardItem newItem = Instantiate(itemPrefab, transform);
            newItem.SetSprite(itemChoosed[i]);
        }
    }
}