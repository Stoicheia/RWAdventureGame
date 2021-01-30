using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public AudioSource dialogueSource;
    public DialogueSubtitles dialogueSubtitles;

    public void PlayDialogue(DialogueLine dialogue)
    {
        dialogueSource.PlayOneShot(dialogue.Audio);
        dialogueSubtitles.SetText(dialogue.Subtitles);
    }
}
