using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUser : MonoBehaviour
{
    private Inventory playerInventory;

    [SerializeField] private float interactionRadius;

    private void Start()
    {
        playerInventory = GlobalStats.instance.PlayerInventory;
        if (playerInventory == null)
        {
            Debug.LogError("Player inventory not found", this);
        }
    }
}
