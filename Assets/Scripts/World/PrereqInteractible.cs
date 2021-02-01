using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrereqInteractible : InteractibleObject
{
    [SerializeField] private Item prereqItem;
    [SerializeField] private List<Task> prereqTasks;

    protected override void DetectHit()
    {
        if (!GlobalStats.instance.PlayerInventory.HasItem(prereqItem)) return;
        if (!GlobalStats.instance.GetComponent<TaskManager>().TasksCompleted(prereqTasks)) return;
        base.DetectHit();
    }
}
