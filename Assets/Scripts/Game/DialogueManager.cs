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

    public bool inDialogue;

    private ClickToMoveController player;

    public PostProcessVolume memoryFilter;
    private MemoryNoises mNoises;

    private float lastSkipTime;

    private void Awake()
    {
        currentPlayIndex = 0;
        inDialogue = false;
    }

    private void Start()
    {
        player = FindObjectOfType<ClickToMoveController>();
        dialogueSource.loop = false;
        if(mNoises!=null)
            mNoises = memoryFilter.GetComponent<MemoryNoises>();
    }

    private void OnEnable()
    {
        currentPlayIndex = 0;
        if(nextLineButton!=null)
            nextLineButton.onClick.AddListener(TryPlayNext);
    }

    private void OnDisable()
    {
        if(nextLineButton!=null)
            nextLineButton.onClick.RemoveAllListeners();
    }

    public void PlayDialogue(DialogueLine dialogue)
    {
        dialogueSource.clip = dialogue.Audio;
        dialogueSource.Play();
        dialogueSubtitles.SetText(dialogue.Subtitles.Replace("\r", ""));
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
        inDialogue = true;
        PlayDialogue(toPlay.lines[currentPlayIndex]);
        currentPlayIndex++;
    }

    public void EnableDialogue(DialogueSequence lines)
    {
        if (inDialogue) return;

        if (lines.memory)
        {
            if (mNoises != null)
            {
                mNoises.Begin();
                mNoises.Play();
            }

            memoryFilter.weight = 1;
        }
        
        if(player!=null)
            player.moveEnabled = false;
        currentPlayIndex = 0;
        toPlay = lines;
        inDialogue = true;
        if(nextLineButton!=null)
            nextLineButton.gameObject.SetActive(true);
        PlayNext();
    }

    public void KillDialogue()
    {
        if(mNoises!=null) mNoises.End();
        player.moveEnabled = true;
        StartCoroutine(TimeToExitDialogue(0.01f));
        memoryFilter.weight = 0;
        if(nextLineButton!=null)
            nextLineButton.gameObject.SetActive(false);
        dialogueSubtitles.DeleteText();
    }

    IEnumerator PlayAfterSequence(float t, DialogueLine d)
    {
        yield return new WaitForSeconds(t);
        PlayDialogue(d);
    }

    IEnumerator TimeToExitDialogue(float t)
    {
        yield return null;
        yield return new WaitForSeconds(t);
        inDialogue = false;
    }
}
