using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData instance;

    public static SaveData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveData();
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<InventorySlotID> playerItemIDs;
}

[System.Serializable]
public class InventorySlotID
{
    public int itemID;
    public int quantity;

    public InventorySlotID(int i, int q)
    {
        itemID = i;
        quantity = q;
    }

    public InventorySlotID()
    {
        itemID = -1;
        quantity = 0;
    }
}