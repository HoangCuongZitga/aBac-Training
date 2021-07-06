using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using System.Linq;

public class ScrollerItemCarryController : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<ItemCarryData> _data;
    public EnhancedScroller myScroller;
    public ItemCarry animalCellViewPrefab;
    private List<Sprite> _sprites;

    void Start()
    {
        _sprites = Resources.LoadAll<Sprite>("Equipped").ToList();


        _data = new List<ItemCarryData>();

        string[] itemname = new string[] {"mũ", "áo", "vũ khí"};

        for (int i = 0; i < _sprites.Count; i++)
        {
            _data.Add(new ItemCarryData()
            {
                itemName = itemname[i],
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
        ItemCarry cellView = scroller.GetCellView(animalCellViewPrefab) as ItemCarry;
        cellView.SetData(_data[dataIndex]);
        return cellView;
    }
}