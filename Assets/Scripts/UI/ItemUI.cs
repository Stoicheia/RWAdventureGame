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
    public delegate void PointerAction(ItemUI iu);

    public static event PointerAction OnHover;
    public static event PointerAction OnExitHover;
    public static event PointerAction OnClicked;
    public static event PointerAction OnDropped;

    [SerializeField] private InventorySlot slotItem;
    public InventorySlot DisplayedItem => slotItem;

    public int SlotID;

    [SerializeField] private Image itemRenderer;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    [SerializeField] private Sprite transparentImage;

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
        itemQuantity.text = slotItem.quantity == 1 ? "" : slotItem.quantity.ToString();
    }

    public void Display(InventorySlot toDisplay)
    {
        slotItem = toDisplay ?? new InventorySlot();
        Refresh();
    }

    public void DisplayNull()
    {
        itemRenderer.sprite = transparentImage;
        itemQuantity.text = "";
    }
    
    public void Click()
    {
        OnClicked?.Invoke(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExitHover?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropped?.Invoke(this);
    }
    
}
