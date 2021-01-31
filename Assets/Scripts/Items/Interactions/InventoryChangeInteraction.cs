using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Interaction", menuName = "Interactions/Inventory Interaction")]
public class InventoryChangeInteraction : ItemObjectInteraction
{
    [SerializeField] private List<Item> toRemove;
    [SerializeField] private List<Item> toAdd;


    public override void Act(InteractibleObject @from)
    {
        Inventory mainInv = GlobalStats.instance.PlayerInventory;
        Debug.Log(mainInv.ItemSlots.Count);

        foreach (var item in toRemove)
        {
            mainInv.DeleteItem(item);
        }

        foreach (var item in toAdd)
        {
            mainInv.AddItem(item);
        }
    }
}
