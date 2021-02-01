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
    public Transform highlighter;
    private Transform myHighlighter;
    public Transform transparentHighlighter;
    private Transform myTransparentHighlighter;

    private ItemUI selectedItem;

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
        ItemUI.OnDropped += Rearrange;
        ItemUI.OnHover += HoverSelect;
        ItemUI.OnExitHover += HoverUnselect;
        ItemUI.OnClicked += Select;
        inventory.OnUpdate += DisplayInventory;
    }

    private void OnDisable()
    {
        selectedItem = null;
        
        ItemUI.OnHover -= ChangeItemInfo;
        ItemUI.OnDropped -= Rearrange;
        ItemUI.OnHover -= HoverSelect;
        ItemUI.OnExitHover -= HoverUnselect;
        ItemUI.OnClicked -= Select;
        inventory.OnUpdate -= DisplayInventory;
    }

    private void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        myHighlighter = Instantiate(highlighter, canvas.transform);
        myHighlighter.gameObject.SetActive(false);
        
        myTransparentHighlighter = Instantiate(transparentHighlighter, canvas.transform);
        myTransparentHighlighter.gameObject.SetActive(false);

        inventory = GlobalStats.instance.PlayerInventory;

        DisplayInventory();
    }

    private void Update()
    {
        DisplayInventory();
    }

    private void Select(ItemUI toHighlight)
    {
        if (selectedItem == toHighlight)
        {
            print("hey");
            foreach (var interaction in selectedItem.DisplayedItem.item.itemClickInteractions)
            {
                interaction.Act(null);
            }
        }
        
        selectedItem = toHighlight;
        if (selectedItem.DisplayedItem == null)
        {
            selectedItem = null;
            return;
        }
        myHighlighter.gameObject.SetActive(true);
        myHighlighter.position = selectedItem.transform.position;
        myHighlighter.SetParent(toHighlight.transform);
    }

    public void UnselectAll()
    {
        selectedItem = null;
        myHighlighter.gameObject.SetActive(false);
    }

    public void DisplayInventory()
    {
        ClearAll();
        for(int i=0; i<inventory.maxItems; i++)
        {
            if (inventory.ItemSlots.Count <= i){return;}
            if (itemsToDisplay.Count <= i){return;}
            
            itemsToDisplay[i].Display(inventory.ItemSlots[i]);
        }
    }

    void ChangeItemInfo(ItemUI ui)
    {
        if (ui.DisplayedItem.item == null) return;
        description.SetText(ui.DisplayedItem.item.Description);
        itemName.SetText(ui.DisplayedItem.item.ItemName);
        itemImage.sprite = ui.DisplayedItem.item.SketchSprite;
    }

    void Rearrange(ItemUI ui)
    {
        if (ItemDragHandler.from == null) return;
        UnselectAll();
        int oldSlot = ItemDragHandler.from.SlotID;
        
        inventory.Swap(oldSlot, ui.SlotID);
    }

    void ClearAll()
    {
        foreach (var slot in itemsToDisplay)
        {
            slot.DisplayNull();
        }
    }

    void HoverSelect(ItemUI ui)
    {
        if (ui.DisplayedItem == null) return;
        if (ui.DisplayedItem.item == null) return;
        myTransparentHighlighter.gameObject.SetActive(true);
        myTransparentHighlighter.position = ui.transform.position;
        myTransparentHighlighter.SetParent(ui.transform);
    }

    void HoverUnselect(ItemUI ui)
    {
        myTransparentHighlighter.gameObject.SetActive(false);
    }
}
