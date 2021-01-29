using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void PointerAction(int s, InventorySlot i);

    public static event PointerAction OnHover;
    public static event PointerAction OnExitHover;
    
    [SerializeField] private InventorySlot slotItem;
    public InventorySlot DisplayedItem => slotItem;

    public int SlotID;

    public Image itemRenderer;
    public TextMeshProUGUI itemName; //not necessary?
    public TextMeshProUGUI itemQuantity;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover?.Invoke(SlotID, slotItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExitHover?.Invoke(SlotID, slotItem);
    }
}
