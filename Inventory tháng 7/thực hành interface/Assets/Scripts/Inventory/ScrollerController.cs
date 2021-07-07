using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnhancedUI.EnhancedScroller;

public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private Popup popup;
    public List<Item> listItems;
    private List<Sprite> _sprites;
    public List<ItemData> data;
    public EnhancedScroller myScroller;
    public Item animalCellViewPrefab;

    void Awake()
    {
        _sprites = Resources.LoadAll<Sprite>("Item_Prototype2").ToList();
        data = new List<ItemData>();

        for (int i = 0; i < _sprites.Count; i++)
        {
            data.Add(new ItemData()
            {
                nameItem = _sprites[i].name,
                isEquipped = false,
                itemType = _sprites[i].name.Substring(0, _sprites[i].name.Length - 1)
            });
        }

        myScroller.Delegate = this;
        myScroller.ReloadData();
    }

    public void ReLoadData(Item item)
    {
        if (item != null && item.itemData.isEquipped == true)
        {
            _sprites = _sprites.Where(e => e.name != item.itemData.nameItem).ToList();
            data.Remove(item.itemData);
            listItems.Remove(item);
            Debug.Log("Is Equipped !!");
        }

        if (item != null && item.itemData.isEquipped == false)
        {
            Sprite sprite = Resources.Load<Sprite>("Item_Prototype2/" + item.itemData.nameItem);

            Item cellView = myScroller.GetCellView(animalCellViewPrefab) as Item;
            cellView.SetData(item.itemData);
            cellView.SetPopup(popup);
            cellView.SetSprite(sprite);
            listItems.Add(cellView);
            _sprites.Add(sprite);
            data.Add(item.itemData);
            Debug.Log("NO !! Equipped !!");
        }

        // data = new List<ItemData>();
        // for (int i = 0; i < _sprites.Count; i++)
        // {
        //     data.Add(new ItemData()
        //     {
        //         nameItem = _sprites[i].name,
        //         isEquipped = false,
        //         itemType = _sprites[i].name.Substring(0, _sprites[i].name.Length - 1)
        //     });
        // }
     //   myScroller.Delegate = this;
        myScroller.ReloadData();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return data.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        Item cellView = scroller.GetCellView(animalCellViewPrefab) as Item;
        cellView.SetData(data[dataIndex]);
        cellView.SetPopup(popup);
        cellView.SetSprite(_sprites[dataIndex]);
        listItems.Add(cellView);
        return cellView;
    }


    public void Change()
    {
        
    }
}