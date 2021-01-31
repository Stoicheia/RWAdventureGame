using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    private DialogueManager dialogueSource;

    private void Start()
    {
        dialogueSource = FindObjectOfType<DialogueManager>();
    }


    public void SetDialogue(DialogueSequence sequence)
    {
        dialogueSource.EnableDialogue(sequence);
    }

    public void SetDialogueAfter(DialogueSequence sequence, float f)
    {
        StartCoroutine(WaitSequence(sequence, f));
    }

    IEnumerator WaitSequence(DialogueSequence s, float f)
    {
        yield return new WaitForSeconds(f);
        dialogueSource.EnableDialogue(s);
    }

    public bool InDialogue()
    {
        return dialogueSource.inDialogue;
    }
    
}
