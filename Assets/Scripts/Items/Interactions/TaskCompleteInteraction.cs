using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task Interaction", menuName = "Interactions/Task Interaction")]
public class TaskCompleteInteraction : ItemObjectInteraction
{

    [SerializeField] private Task toComplete;

    public override void Act(InteractibleObject from)
    {
        TaskManager.instance.CompleteTask(toComplete);
    }

    
}
