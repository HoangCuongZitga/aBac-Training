using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseController : CarriesItemController
{
    public override bool AddItem(Item item)
    {
        ItemView itemTypePos = listItemView.Find(e => e.GetData().itemName.Length == 3);
        if (itemTypePos == null) itemTypePos = listItemView[0];
        //replace item if it's the same type
        if (itemTypePos.GetData().itemName.Length > 3) _scrollerController.AddItem(itemTypePos.GetData());
        itemTypePos.SetData(item);
        itemTypePos.ShowButton();
        return true;
    }
}    