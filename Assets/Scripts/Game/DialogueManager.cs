using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public AudioSource dialogueSource;
    public DialogueSubtitles dialogueSubtitles;
    public Button nextLineButton;

    public DialogueSequence toPlay;
    public int currentPlayIndex;

    private bool inDialogue;

    private ClickToMoveController player;

    public PostProcessVolume memoryFilter;

    private void Awake()
    {
        currentPlayIndex = 0;
        inDialogue = false;
    }

    private void Start()
    {
        player = FindObjectOfType<ClickToMoveController>();
        KillDialogue();
    }

    private void OnEnable()
    {
        currentPlayIndex = 0;
        nextLineButton.onClick.AddListener(PlayNext);
    }

    private void OnDisable()
    {
        nextLineButton.onClick.RemoveAllListeners();
    }

    public void PlayDialogue(DialogueLine dialogue)
    {
        dialogueSource.PlayOneShot(dialogue.Audio);
        dialogueSubtitles.SetText(dialogue.Subtitles);
        if (!inDialogue)
        {
            dialogueSubtitles.DeleteTextAfter(3.1f);
        }
    }

    public void PlayDialogueAfter(float t, DialogueLine d)
    {
        StartCoroutine(PlayAfterSequence(t,d));
    }

    public void PlayNext()
    {
        if (currentPlayIndex >= toPlay.lines.Count)
        {
            KillDialogue();
            return;
        }
        PlayDialogue(toPlay.lines[currentPlayIndex]);
        currentPlayIndex++;
    }

    public void EnableDialogue(DialogueSequence lines)
    {
        if (inDialogue) return;

        if (lines.memory)
        {
            memoryFilter.weight = 1;
        }
        
        player.moveEnabled = false;
        currentPlayIndex = 0;
        toPlay = lines;
        inDialogue = true;
        nextLineButton.gameObject.SetActive(true);
        PlayNext();
    }

    void KillDialogue()
    {
        player.moveEnabled = true;
        inDialogue = false;
        memoryFilter.weight = 0;
        nextLineButton.gameObject.SetActive(false);
        dialogueSubtitles.DeleteText();
    }

    IEnumerator PlayAfterSequence(float t, DialogueLine d)
    {
        yield return new WaitForSeconds(t);
        PlayDialogue(d);
    }
}
