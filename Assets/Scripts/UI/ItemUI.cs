using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public delegate void PointerAction(int s, InventorySlot i);

    public static event PointerAction OnHover;
    public static event PointerAction OnExitHover;
    public static event PointerAction OnClicked;
    public static event PointerAction OnDropped;
    
    [SerializeField] private InventorySlot slotItem;
    public InventorySlot DisplayedItem => slotItem;

    public int SlotID;

    [SerializeField] private Image itemRenderer;
    [SerializeField] private TextMeshProUGUI itemName; //not necessary?
    [SerializeField] private TextMeshProUGUI itemQuantity;

    private LayoutGroup myLayoutGroup;

    private void Awake()
    {
        slotItem = new InventorySlot();
    }

    private void OnEnable()
    {
        myLayoutGroup = transform.parent.GetComponent<LayoutGroup>();
    }

    private void Refresh()
    {
        if (slotItem.item == null)
        {
            DisplayNull();
            return;
        }
        itemRenderer.sprite = slotItem.item.InventorySprite;
        itemName.text = slotItem.item.ItemName;
        itemQuantity.text = slotItem.quantity.ToString();
    }

    public void Display(InventorySlot toDisplay)
    {
        slotItem = toDisplay ?? new InventorySlot();
        Refresh();
    }

    public void DisplayNull()
    {
        itemRenderer.sprite = null;
        itemName.text = "";
        itemQuantity.text = "";
    }
    
    public void Click()
    {
        OnClicked?.Invoke(SlotID, slotItem);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover?.Invoke(SlotID, slotItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExitHover?.Invoke(SlotID, slotItem);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropped?.Invoke(SlotID, slotItem);
    }

    public void Highlight()
    {
        itemName.color = new Color(1, 1, 0, 1);
    }

    public void Unhighlight()
    {
        itemName.color = new Color(0, 0, 0, 1);
    }
}
