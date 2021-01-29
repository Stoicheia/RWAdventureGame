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

    public FadeUI description;
    public FadeUI itemName;
    public Image itemImage;

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

        ItemUI.OnHover += ChangeItemInfo;
        //ItemUI.OnExitHover += HideDescription;
        ItemUI.OnDropped += Rearrange;
        ItemUI.OnClicked += SelectItem;
        inventory.OnUpdate += DisplayInventory;
    }

    private void OnDisable()
    {
        selectedItem = null;
        
        ItemUI.OnHover -= ChangeItemInfo;
        //ItemUI.OnExitHover -= HideDescription;
        ItemUI.OnDropped -= Rearrange;
        ItemUI.OnClicked -= SelectItem;
        inventory.OnUpdate -= DisplayInventory;
    }

    private void Start()
    {
        DisplayInventory();
    }

    void RefreshHighlights()
    {
        foreach (ItemUI slot in itemsToDisplay)
        {
            if (slot.DisplayedItem.item == null) continue;
            if (selectedItem == null)
            {
                slot.itemName.color = new Color(0, 0, 0, 1);
                continue;
            }
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
        ClearAll();
        for(int i=0; i<inventory.maxItems; i++)
        {
            if (inventory.ItemSlots.Count <= i){RefreshHighlights(); return;}
            if (itemsToDisplay.Count <= i){RefreshHighlights(); return;}
            itemsToDisplay[i].Display(inventory.ItemSlots[i]);
        }
        RefreshHighlights();
    }

    void ChangeItemInfo(int id, InventorySlot invSlot)
    {
        if (invSlot.item == null) return;
        description.SetText(invSlot.item.Description);
        itemName.SetText(invSlot.item.ItemName);
        itemImage.sprite = invSlot.item.SketchSprite;
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

    void ClearAll()
    {
        foreach (var slot in itemsToDisplay)
        {
            slot.DisplayNull();
        }
    }
}
