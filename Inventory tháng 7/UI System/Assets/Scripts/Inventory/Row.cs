using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;


namespace EnhancedScrollerDemos.GridSimulation
{
    public class Row : EnhancedScrollerCellView
    {
        public ItemView_InvenTory[] itemRows;
        private Action<Item> ItemOnClickDelegate;

        private void Start()
        {
            foreach (ItemView_InvenTory item in itemRows)
            {
                item.SetActionOnClick(ItemOnClickDelegate);
            }
        }


        public void SetItemOnClickDelegate(Action<Item> method)
        {
            ItemOnClickDelegate = method;
        }

        public void SetData(ref List<Item> data, int startingIndex)
        {
            // loop through the sub cells to display their data (or disable them if they are outside the bounds of the data)
            for (var i = 0; i < itemRows.Length; i++)
            {
                // if the sub cell is outside the bounds of the data, we pass null to the sub cell
                itemRows[i].SetData(startingIndex + i < data.Count ? data[startingIndex + i] : null);
                itemRows[i].SetActionOnClick(ItemOnClickDelegate);
            }
        }
    }
}