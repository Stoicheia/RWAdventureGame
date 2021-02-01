using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUser : MonoBehaviour
{
    private Inventory playerInventory;

    [SerializeField] public float interactionRadius;
    public bool hasShoes; //whatever

    private void Start()
    {
        if(GlobalStats.instance==null) Debug.LogError("Please Move GlobalStats to this Scene");
        playerInventory = GlobalStats.instance.PlayerInventory;
        if (playerInventory == null)
        {
            Debug.LogError("Player inventory not found", this);
        }
    }

    private void Update()
    {
        
    }
}
