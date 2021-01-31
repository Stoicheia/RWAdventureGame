using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "Simple Audio Interaction", menuName = "Interactions/Simple Audio Interaction")]
public class SimpleAudioInteraction : ItemObjectInteraction
{

    public List<AudioClip> audio;

    public override void Act(InteractibleObject @from)
    {
        FindObjectOfType<DialogueManager>().dialogueSource.PlayOneShot(audio[UnityEngine.Random.Range(0,audio.Count)]);
    }
}
