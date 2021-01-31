using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : SwappableObject
{
    [SerializeField] private DialogueSequence bridgeFirstDia;
    public override void InteractWithObject()
    {
        if (base.timesInteracted == 0)
        {
            FindObjectOfType<DialogueSystem>().SetDialogue(bridgeFirstDia);
            timesInteracted--;
        }
        else
        {
            base.InteractWithObject();
        }
    }
}
