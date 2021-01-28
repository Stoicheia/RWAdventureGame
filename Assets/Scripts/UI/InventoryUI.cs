using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    
    [SerializeField] private Transform[] itemSlotsContainers;
    private List<ItemUI> itemsToDisplay;

    private void Awake()
    {
        itemsToDisplay = new List<ItemUI>();
        foreach (Transform itemSlotsContainer in itemSlotsContainers)
        {
            foreach (Transform t in itemSlotsContainer)
            {
                ItemUI itemSlot = t.GetComponent<ItemUI>();
                if (itemSlot != null)
                {
                    itemsToDisplay.Add(itemSlot);
                }
            }
        }
    }

    public void Refresh()
    {
        foreach (ItemUI slot in itemsToDisplay)
        {
            slot.Refresh();
        }
    }

    public void DisplayInventory()
    {
        int inventorySize = inventory.ItemCount;
        for(int i=0; i<inventory.maxItems; i++)
        {
            itemsToDisplay[i].Display(inventory.ItemSlots[i]);
        }
    }
}
