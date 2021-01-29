using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    
    [SerializeField] private Transform[] itemSlotsContainers;
    private List<ItemUI> itemsToDisplay;

    public ItemDescriptionUI description;

    private void Awake()
    {
        itemsToDisplay = new List<ItemUI>();
        int k = 0;
        for (int i=0; i<itemSlotsContainers.Length; i++)
        {
            for (int j=0; j<itemSlotsContainers[i].childCount; j++)
            {
                ItemUI itemSlot = itemSlotsContainers[i].GetChild(j).GetComponent<ItemUI>();
                if (itemSlot != null)
                {
                    itemsToDisplay.Add(itemSlot);
                    itemSlot.SlotID = k++;
                }
            }
        }

        k = 0;
    }

    private void OnEnable()
    {
        DisplayInventory();

        ItemUI.OnHover += ShowDescription;
        ItemUI.OnExitHover += HideDescription;
    }

    private void OnDisable()
    {
        ItemUI.OnHover -= ShowDescription;
        ItemUI.OnExitHover -= HideDescription;
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
            if (inventory.ItemSlots.Count <= i) return;
            itemsToDisplay[i].Display(inventory.ItemSlots[i]);
        }
    }

    void ShowDescription(int id, InventorySlot invSlot)
    {
        if (invSlot == null) return;
        if (invSlot.item == null) return;
        description.SetText(invSlot.item.Description);
    }

    void HideDescription(int n, InventorySlot o)
    {
        description.SetText("");
    }
}
