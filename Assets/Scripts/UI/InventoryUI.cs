using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public delegate void ItemAction(InventorySlot item, InventorySlot otherItem);

    public static event ItemAction OnItemUse;
    
    [SerializeField] private Inventory inventory;
    
    [SerializeField] private Transform[] itemSlotsContainers;
    private List<ItemUI> itemsToDisplay;

    public ItemDescriptionUI description;

    private InventorySlot selectedItem;
    private int selectedSlot = -1;

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
                    itemSlot.DisplayNull(); 
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
        ItemUI.OnDropped += Rearrange;
        ItemUI.OnClicked += SelectItem;
        inventory.OnUpdate += DisplayInventory;
    }

    private void OnDisable()
    {
        ItemUI.OnHover -= ShowDescription;
        ItemUI.OnExitHover -= HideDescription;
        ItemUI.OnDropped -= Rearrange;
        ItemUI.OnClicked -= SelectItem;
        inventory.OnUpdate -= DisplayInventory;
    }

    void Refresh()
    {
        foreach (ItemUI slot in itemsToDisplay)
        {
            slot.Refresh();
            if (slot.DisplayedItem.item == null) continue;
            if (selectedItem == null) continue;
            if (selectedItem.item == null) continue;
            if (selectedItem.ItemID == slot.DisplayedItem.ItemID && selectedSlot == slot.SlotID)
            {
                slot.itemName.color = new Color(1, 1, 0, 1);
            }
            else
            {
                slot.itemName.color = new Color(0, 0, 0, 1);
            }
        }
    }

    public void DisplayInventory()
    {
        int inventorySize = inventory.ItemCount;
        for(int i=0; i<inventory.maxItems; i++)
        {
            if (inventory.ItemSlots.Count <= i){Refresh(); return;}
            if (itemsToDisplay.Count <= i){Refresh(); return;}
            itemsToDisplay[i].Display(inventory.ItemSlots[i]);
        }
        Refresh();
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

    void Rearrange(int newSlot, InventorySlot oldItem)
    {
        if (ItemDragHandler.from == null) return;
        int oldSlot = ItemDragHandler.from.SlotID;
        
        inventory.Swap(oldSlot, newSlot);
    }

    void SelectItem(int id, InventorySlot itemToUse)
    {
        if (itemToUse.item == null) return;
        if (selectedItem == null)
        {
            selectedItem = itemToUse;
            selectedSlot = id;
            DisplayInventory();
        }
        else
        {
            OnItemUse?.Invoke(itemToUse, selectedItem);
            selectedItem = null;
            selectedSlot = -1;
            DisplayInventory();
        }
    }
}
