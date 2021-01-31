using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prereq Item", menuName = "New Prereq Item")]
public class ItemCombAct : Item
{
    public Item complement;
    
     public List<ItemObjectInteractionPair> neutralItemInteraction;
     public List<ItemObjectInteractionPair> successfulItemIneraction;
}
