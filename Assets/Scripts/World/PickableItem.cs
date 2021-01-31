using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : InteractibleObject
{
    [SerializeField] private Item associatedItem;
    [SerializeField] private bool destroyWorldObject;

    public override void InteractWithObject()
    {
        base.InteractWithObject();
        
        GlobalStats.instance.PlayerInventory.AddItem(associatedItem);
        if (destroyWorldObject)
        {
            Destroy(gameObject);
        }
    }
}
