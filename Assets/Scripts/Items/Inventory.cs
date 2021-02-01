using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    public delegate void UpdateAction();

    public event UpdateAction OnUpdate;

    [SerializeField] private List<InventorySlot> itemSlots;
    public int maxItems;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public List<InventorySlot> ItemSlots
    {
        get
        {
            return itemSlots; 
        }
        set
        {
            itemSlots = value;
            OnUpdate?.Invoke();
        }
    }

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
            if (itemSlots[i] == null) continue;
            if (itemSlots[i].ItemID == item.ItemID)
            {
                itemSlots[i].quantity += numberToAdd;
                OnUpdate?.Invoke();
                return;
            }
        }

        if (ItemCount >= maxItems)
        {
            Debug.Log("Inventory full", this);
            OnUpdate?.Invoke();
            return;
        }
        
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i] == null || itemSlots[i].item == null)
            {
                itemSlots[i] = new InventorySlot(item, numberToAdd);
                OnUpdate?.Invoke();
                return;
            }
        }   
        itemSlots.Add(new InventorySlot(item, numberToAdd));
        OnUpdate?.Invoke();
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
            if (invSlot == null) continue;
            if(invSlot.item == null) continue;
            if (invSlot.ItemID == itemID)
            {
                invSlot.quantity -= quantity;
                if (invSlot.quantity <= 0)
                {
                    itemSlots[i].item = null;
                }
                OnUpdate?.Invoke();
                return;
            }
        }
        OnUpdate?.Invoke();
    }
    
    public void DeleteItem(int itemID)
    {
        DeleteItem(itemID, 1);
    }
    
    public void DeleteItem(Item item, int quantity)
    {
        for(int i=0; i<itemSlots.Count; i++)
        {
            InventorySlot invSlot = itemSlots[i];
            if (invSlot == null) continue;
            if(invSlot.item == null) continue;
            if (invSlot.ItemID == item.ItemID)
            {
                invSlot.quantity -= quantity;
                if (invSlot.quantity <= 0)
                {
                    itemSlots[i].item = null;
                }
                OnUpdate?.Invoke();
                return;
            }
        }
        OnUpdate?.Invoke();
    }
    
    public void DeleteItem(Item item)
    {
        DeleteItem(item, 1);
    }

    public void Swap(int a, int b)
    {
        if (Mathf.Max(a, b) >= itemSlots.Count)
        {
            int c = itemSlots.Count-1;
            for (int i = 0; i < Mathf.Max(a, b) - c; i++)
            {
                itemSlots.Add(null);
            }
        }

        InventorySlot temp = itemSlots[a];
        itemSlots[a] = itemSlots[b];
        itemSlots[b] = temp;
        OnUpdate?.Invoke();
    }

    public bool HasItem(Item item)
    {
        if (item == null) return true;
        foreach (var i in itemSlots)
        {
            if (i == null) continue;
            if (i.item == null) continue;
            if (i.item.ItemID == item.ItemID)
                return true;
        }

        return false;
    }

    /*void UseItem(InventorySlot firstItem, InventorySlot secondItem)
    {
        if (firstItem.item == null || secondItem.item == null) return;
        int id1 = firstItem.ItemID;
        int id2 = secondItem.ItemID;
        firstItem.item.Act(secondItem.item);
        if (firstItem.item.Consumable)
        {
            DeleteItem(firstItem.item);
        }

        if (secondItem.item == null) return;

        if (secondItem.item.Consumable && id1!=id2)
        {
            DeleteItem(secondItem.item);
        }
    }*/
   
}
