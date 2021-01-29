using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> itemSlots;
    public int maxItems;

    public List<InventorySlot> ItemSlots => itemSlots;
    public int ItemCount
    {
        get
        {
            int c = 0;
            foreach (InventorySlot i in itemSlots)
            {
                if (i != null)
                {
                    c++;
                }
            }

            return c;
        }
    }

    public void AddItem(Item item, int numberToAdd)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].ItemID == item.ItemID)
            {
                itemSlots[i].quantity += numberToAdd;
                return;
            }
        }

        if (ItemCount >= maxItems)
        {
            Debug.Log("Inventory full", this);
            return;
        }
        
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i] == null)
            {
                itemSlots[i] = new InventorySlot(item, numberToAdd);
                return;
            }
        }
        
        itemSlots.Add(new InventorySlot(item, numberToAdd));
    }

    public void AddItem(Item item)
    {
        AddItem(item, 1);
    }

    public void AddItem(int itemID)
    {
        //TODO: look up item in database
    }

    public void DeleteItem(int itemID, int quantity)
    {
        for(int i=0; i<itemSlots.Count; i++)
        {
            InventorySlot invSlot = itemSlots[i];
            if (invSlot.ItemID == itemID)
            {
                invSlot.quantity -= quantity;
                if (invSlot.quantity <= 0)
                {
                    itemSlots[i] = null;
                }

                return;
            }
        }
    }
    
    public void DeleteItem(int itemID)
    {
        DeleteItem(itemID, 1);
    }
   
}
