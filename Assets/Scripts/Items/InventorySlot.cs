using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;

    public int ItemID => item == null ? -1 : item.ItemID;

    public InventorySlot(Item i, int q)
    {
        item = i;
        quantity = q;
    }

    public InventorySlot(Item i)
    {
        item = i;
        quantity = 1;
    }

    public InventorySlot()
    {
        item = null;
        quantity = 0;
    }
}
