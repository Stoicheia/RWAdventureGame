using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseReader : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    
    private static DatabaseReader instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Item GetItemByID(int id)
    {
        foreach (var item in instance.itemDatabase.gameItems)
        {
            if (item.ItemID == id)
            {
                return item;
            }
        }

        return null;
    }
}
