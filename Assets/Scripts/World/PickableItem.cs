using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : InteractibleObject
{
    [SerializeField] private Item associatedItem;
    [SerializeField] private DialogueSequence associatedDialogue;
    [SerializeField] private bool destroyWorldObject;

    public override void InteractWithObject()
    {
        base.InteractWithObject();
        
        GlobalStats.instance.PlayerInventory.AddItem(associatedItem);
        if(associatedDialogue!=null) 
            FindObjectOfType<DialogueSystem>().SetDialogue(associatedDialogue);
        if (destroyWorldObject)
        {
            Destroy(gameObject);
        }
    }
}
