using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public void Save()
    {
        SaveData.Instance.playerItemIDs = InventoryToIDList(GlobalStats.instance.PlayerInventory.ItemSlots);
        SerializationManager.Save("main", SaveData.Instance);
    }

    public void Load()
    {
         SaveData.Instance = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/main.save");
         GlobalStats.instance.PlayerInventory.ItemSlots = IDListToInventory(SaveData.Instance.playerItemIDs);
    }

    public List<InventorySlotID> InventoryToIDList(List<InventorySlot> inventory)
    {
        List<InventorySlotID> invSlotsIDs = new List<InventorySlotID>();
        foreach (var slot in inventory)
        {
            if (slot == null)
            {
                invSlotsIDs.Add(new InventorySlotID());
                continue;
            }

            if (slot.item == null)
            {
                invSlotsIDs.Add(new InventorySlotID());
                continue;
            }
            invSlotsIDs.Add(new InventorySlotID(slot.ItemID, slot.quantity));
        }

        return invSlotsIDs;
    }

    public List<InventorySlot> IDListToInventory(List<InventorySlotID> idList)
    {
        List<InventorySlot> newInventory = new List<InventorySlot>();
        foreach (var idQuantPair in idList)
        {
            int id = idQuantPair.itemID;
            int quantity = idQuantPair.quantity;
            Item item = DatabaseReader.GetItemByID(id);
            InventorySlot toAdd;
            if (item == null)
            {
                toAdd = new InventorySlot(); 
            }
            else
            {
                toAdd = new InventorySlot(DatabaseReader.GetItemByID(id), quantity);
            }
            newInventory.Add(toAdd);
        }

        return newInventory;
    }
}
