using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrereqInteractible : InteractibleObject
{
    [SerializeField] private Item prereqItem;

    protected override void DetectHit()
    {
        if (!GlobalStats.instance.PlayerInventory.HasItem(prereqItem)) return;
        base.DetectHit();
    }
}
