using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : SwappableObject
{
    [SerializeField] private DialogueSequence bridgeFirstDia;
    [SerializeField] private ItemObjectInteraction taskComp;
    private bool firstDia;

    protected override void Start()
    {
        base.Start();

        firstDia = false;
    }

    public override void InteractWithObject()
    {
        if (!firstDia && !swapped)
        {
            FindObjectOfType<DialogueSystem>().SetDialogue(bridgeFirstDia);
            taskComp.Act(this);
            firstDia = true;
        }
        else
        {
            base.InteractWithObject();
        }
    }
}
