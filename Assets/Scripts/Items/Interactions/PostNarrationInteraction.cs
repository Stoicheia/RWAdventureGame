using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Postnarration Interaction", menuName = "Interactions/Postnarration Interaction")]
public class PostNarrationInteraction : ItemObjectInteraction
{
    [SerializeField] private FadeBlackInteraction afterThis;

    [SerializeField] private DialogueSequence narration;
    private DialogueSystem dialogueSource;
    public override void Act(InteractibleObject @from)
    {
        dialogueSource = FindObjectOfType<DialogueSystem>();
        float toWait = 1.5f + afterThis.toPlay.length;
        dialogueSource.SetDialogue(narration);
    }
}
