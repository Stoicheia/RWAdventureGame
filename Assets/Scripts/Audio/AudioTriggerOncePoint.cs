using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Collider2D))]
public class AudioTriggerOncePoint : MonoBehaviour
{
    private Collider2D col;
    private ClickToMoveController activePlayer;
    [SerializeField] private DialogueSequence audion;
    [SerializeField] private List<ItemObjectInteraction> events;

    private DialogueSystem dialogueManager;
    
    private bool activated;
    private void Start()
    {
        activated = false;
        col = GetComponent<Collider2D>();
        activePlayer = FindObjectOfType<ClickToMoveController>();
        dialogueManager = FindObjectOfType<DialogueSystem>();
        if(dialogueManager==null) Debug.LogWarning("No dialogue manager found!");
    }

    public void PlayLine()
    {
        if (activated) return;
        
        dialogueManager.SetDialogue(audion);
        foreach (var e in events)
        {
            e.Act(null);
        }
        
        activated = true;
    }
}
