using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent((typeof(AudioSource)))]
public class Speaker : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private List<AudioClipArray> voiceLines;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.loop = false;
    }

    public void PlayClip(string toPlay)
    {
        foreach (var clipArray in voiceLines)
        {
            if(clipArray.clipName == toPlay)
            {
                clipArray.PlayRandom(source);
                return;
            }
        }
        Debug.LogWarning("Audio clip \"" + toPlay + "\" not found!", this);
    }

    public void PlayOneShot(string toPlay)
    {
        foreach (var clipArray in voiceLines)
        {
            if(clipArray.clipName == toPlay)
            {
                clipArray.PlayRandomOneShot(source);
                return;
            }
        }
        Debug.LogWarning("Audio clip \"" + toPlay + "\" not found!", this);
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }
}

[System.Serializable]
public class AudioClipArray
{
    public string clipName;
    public List<AudioClip> clips;

    public void PlayRandom(AudioSource source)
    {
        int clipNumberToPlay = UnityEngine.Random.Range(0,clips.Count);
        source.Stop();
        source.clip = clips[clipNumberToPlay];
        source.Play();
    }

    public void PlayRandomOneShot(AudioSource source)
    {
        int clipNumberToPlay = UnityEngine.Random.Range(0,clips.Count);
        source.PlayOneShot(clips[clipNumberToPlay]);
    }
}