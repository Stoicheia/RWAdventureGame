using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foliage : SwappableObject
{
    [SerializeField] private DialogueSequence firstNoMatterWhat;
    private bool firsted;
    
    protected override void Start()
    {
        base.Start();

        firsted = false;
    }

    public override void InteractWithObject()
    {
        if (!firsted)
        {
            FindObjectOfType<DialogueSystem>().SetDialogue(firstNoMatterWhat);
            firsted = true;
        }
        else
        {
            base.InteractWithObject();
        }
    }
}
