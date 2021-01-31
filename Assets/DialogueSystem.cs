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
    
}
