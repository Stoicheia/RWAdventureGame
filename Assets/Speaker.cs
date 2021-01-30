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
                int clipNumberToPlay = UnityEngine.Random.Range(0,clipArray.clips.Count);
                source.Stop();
                source.clip = clipArray.clips[clipNumberToPlay];
                source.Play();
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
}