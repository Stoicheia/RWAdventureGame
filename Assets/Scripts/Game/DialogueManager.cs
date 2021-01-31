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
    private const float SKIP_COOLDOWN = 0.25f;
    
    public AudioSource dialogueSource;
    public DialogueSubtitles dialogueSubtitles;
    public Button nextLineButton;

    public DialogueSequence toPlay;
    public int currentPlayIndex;

    private bool inDialogue;

    private ClickToMoveController player;

    public PostProcessVolume memoryFilter;

    private float lastSkipTime;

    private void Awake()
    {
        currentPlayIndex = 0;
        inDialogue = false;
    }

    private void Start()
    {
        player = FindObjectOfType<ClickToMoveController>();
        KillDialogue();
        dialogueSource.loop = false;
    }

    private void OnEnable()
    {
        currentPlayIndex = 0;
        nextLineButton.onClick.AddListener(TryPlayNext);
    }

    private void OnDisable()
    {
        nextLineButton.onClick.RemoveAllListeners();
    }

    public void PlayDialogue(DialogueLine dialogue)
    {
        dialogueSource.clip = dialogue.Audio;
        dialogueSource.Play();
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

    void TryPlayNext()
    {
        if (Time.time < lastSkipTime + SKIP_COOLDOWN) return;
        lastSkipTime = Time.time;
        PlayNext();
    }

    public void PlayNext()
    {
        if (currentPlayIndex >= toPlay.lines.Count)
        {
            if (toPlay.nextDialogue != null)
            {
                inDialogue = false;
                EnableDialogue(toPlay.nextDialogue);
            }
            else
            {
                KillDialogue();
            }
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
        
        if(lines.lines.Count>1)
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
