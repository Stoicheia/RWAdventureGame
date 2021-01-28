using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private InventorySlot slotItem;
    public InventorySlot DisplayedItem => slotItem;

    public SpriteRenderer itemRenderer;
    public TextMeshProUGUI itemName; //not necessary?
    public TextMeshProUGUI itemQuantity;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (slotItem == null)
        {
            itemRenderer.sprite = null;
            itemName.text = "";
            return;
        }
        itemRenderer.sprite = slotItem.item.InventorySprite;
        itemName.text = slotItem.item.ItemName;
        itemQuantity.text = slotItem.quantity.ToString();
    }

    public void Display(InventorySlot toDisplay)
    {
        slotItem = toDisplay;
        Refresh();
    }
    
}
