using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObjectInteraction : ScriptableObject
{
    [SerializeField] protected Item interactibleItem;

    public abstract void Act(InteractibleObject from);


    #region Properties

    public Item InteractibleItem
    {
        get => interactibleItem;
        set => interactibleItem = value;
    }

    #endregion
    
}
