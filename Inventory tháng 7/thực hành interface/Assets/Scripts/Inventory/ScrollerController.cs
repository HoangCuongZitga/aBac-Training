using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnhancedUI.EnhancedScroller;

public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<ItemData> _data;
    public EnhancedScroller myScroller;
    public Item animalCellViewPrefab;
    private List<Sprite> _sprites;

    void Start()
    {

        _sprites = Resources.LoadAll<Sprite>("Item_Prototype2").ToList();
        
        
        _data = new List<ItemData>();

        string[] itemname = new string[] {"item1", "item2", "item3", "item4", "item5", "item6"};
        
        for (int i = 0; i < _sprites.Count; i++)
        {
            _data.Add(new ItemData()
            {
                nameItem = itemname[i],
                itemImage = _sprites[i]
            });
        }

        myScroller.Delegate = this;
        myScroller.ReloadData();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int
        dataIndex, int cellIndex)
    {
        Item cellView = scroller.GetCellView(animalCellViewPrefab) as
            Item;
        cellView.SetData(_data[dataIndex]);
        return cellView;
    }
}