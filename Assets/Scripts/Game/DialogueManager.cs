using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public AudioSource dialogueSource;
    public DialogueSubtitles dialogueSubtitles;
    public Button nextLineButton;

    public List<DialogueLine> toPlay;
    public int currentPlayIndex;

    private bool inDialogue;

    private void Awake()
    {
        currentPlayIndex = 0;
        inDialogue = false;
        toPlay = new List<DialogueLine>();
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
        if (currentPlayIndex >= toPlay.Count)
        {
            KillDialogue();
            return;
        }
        PlayDialogue(toPlay[currentPlayIndex]);
        currentPlayIndex++;
    }

    public void EnableDialogue(List<DialogueLine> lines)
    {
        if (inDialogue) return;
        currentPlayIndex = 0;
        toPlay = lines;
        inDialogue = true;
        nextLineButton.gameObject.SetActive(true);
        PlayNext();
    }

    void KillDialogue()
    {
        inDialogue = false;
        nextLineButton.gameObject.SetActive(false);
        dialogueSubtitles.DeleteText();
    }

    IEnumerator PlayAfterSequence(float t, DialogueLine d)
    {
        yield return new WaitForSeconds(t);
        PlayDialogue(d);
    }
}
