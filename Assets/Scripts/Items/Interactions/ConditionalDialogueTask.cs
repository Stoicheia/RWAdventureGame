using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sdsd", menuName = "Conditional Dialogue Interaction")]
public class ConditionalDialogueTask : ItemObjectInteraction
{
    public Task requiredTask;

    public DialogueSequence sequence;

    private DialogueSystem dialogue;

    public override void Act(InteractibleObject @from)
    {
        dialogue = FindObjectOfType<DialogueSystem>();
        if (GlobalStats.instance.GetComponent<TaskManager>().TaskCompleted(requiredTask))
        {
            dialogue.SetDialogue(sequence);
        }
    }
}
