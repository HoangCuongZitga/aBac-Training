using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;

namespace EnhancedScrollerDemos.GridSimulation
{
    public class ScrollerController_Inventory : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller myScroller;
        [SerializeField] private EnhancedScrollerCellView itemViewPrefab;
        [SerializeField] private int numberOfCellsPerRow = 3;
        private List<Item> _data;
        private Database _database;
        private Action<Item> ItemOnClickDelegate;

        void Start()
        {
            myScroller.Delegate = this;
            _data = new List<Item>();
            LoadData();
        }

        private void LoadData()
        {
            foreach (Item item in _database.data.listItemsAreNotCarried)
            {
                _data.Add(item);
            }

            myScroller.ReloadData();
        }
        
         private IEnumerator ReloadData(Item item)
        {
            yield return new WaitForEndOfFrame();
            myScroller.ReloadData();
            if (item != null)
            {
                myScroller.JumpToDataIndex(_data.IndexOf(item));
            }
        }
        //................................... PUBLIC METHODS ...............................
        public void SetDatabase(Database database)
        {
            _database = database;
        }
        public void SetItemOnClickDelegate(Action<Item> method)
        {
            ItemOnClickDelegate = method;
        }
        public virtual void RemoveItem(Item item)
        {
            item.isCarried = true;
            Item itemMustRemove = _data.Find(e => e.ID == item.ID);
            _data.Remove(itemMustRemove);
            StartCoroutine(ReloadData(item));
        }
        
        public virtual void AddItem(Item item)
        {
            item.isCarried = false;
            _data.Add(item);
            StartCoroutine(ReloadData(item));
        }
        //................................... DEFAULT METHODS ...............................

        public virtual float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 100f;
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return Mathf.CeilToInt((float) _data.Count / (float) numberOfCellsPerRow);
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            Row rowView = scroller.GetCellView(itemViewPrefab) as Row;

            rowView.name = "Cell " + (dataIndex * numberOfCellsPerRow).ToString() + " to " +
                           ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();

            rowView.SetData(ref _data, dataIndex * numberOfCellsPerRow);
            rowView.SetItemOnClickDelegate(ItemOnClickDelegate);

            return rowView;
        }
    }
}