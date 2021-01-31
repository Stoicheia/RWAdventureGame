using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractibleObject
{
    [SerializeField] private FadeBlackInteraction bedInteraction;

    public override void InteractWithObject()
    {
        if (base.timesInteracted == 1)
        {
            bedInteraction.Act(this);
        }
        else
        {
            base.InteractWithObject();
        }
    }
}
