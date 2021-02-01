using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bed : InteractibleObject
{
    [SerializeField] private FadeBlackInteraction bedInteraction;

    protected override void Start()
    {
        base.Start();
        
        StartCoroutine(StartDialogueAfterABit(0.1f));
        
    }

    public override void InteractWithObject()
    {
        if (base.timesInteracted == 0)
        {
            bedInteraction.Act(this);
            timesInteracted++;
        }
        else
        {
            base.InteractWithObject();
        }
    }

    IEnumerator StartDialogueAfterABit(float t)
    {
        yield return null;
        yield return new WaitForSeconds(t);
        dialogueSource.SetDialogue(firstTimeDialogue);
    }
}
