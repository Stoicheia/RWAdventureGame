using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class InventoryUser : MonoBehaviour
{
    private Inventory playerInventory;

    private ClickToMoveController controller;

    [SerializeField] public float interactionRadius;
    public bool hasShoes; //whatever

    private void Start()
    {
        controller = GetComponent<ClickToMoveController>();
        if(GlobalStats.instance==null) Debug.LogError("Please Move GlobalStats to this Scene");
        playerInventory = GlobalStats.instance.PlayerInventory;
        if (playerInventory == null)
        {
            Debug.LogError("Player inventory not found", this);
        }
    }

    private void Update()
    {
        controller.SetSpeed(hasShoes ? controller.runSpeed : controller.walkSpeed);
    }
}
