using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObjectInteraction : ScriptableObject
{ 
    public abstract void Act(InteractibleObject from);
}
