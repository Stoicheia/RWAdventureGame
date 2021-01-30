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

    private Dictionary<string, AudioClipArray> _voiceLineMap;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.loop = false;
        _voiceLineMap = new Dictionary<string, AudioClipArray>();
    }

    private void Start()
    {
        // cache the clip arrays into the dictionary.
        foreach (AudioClipArray clipArray in voiceLines)
        {
            _voiceLineMap[clipArray.clipName] = clipArray;
        }
    }

    public void PlayClip(string toPlay)
    {
        if (!_voiceLineMap.ContainsKey(toPlay))
        {
            Debug.LogError("Audio clip \"" + toPlay + "\" not found!", this);
            return;
        }

        var clipArray = _voiceLineMap[toPlay];
        clipArray.PlayRandom(source);
    }

    public void PlayOneShot(string toPlay)
    {
        if (!_voiceLineMap.ContainsKey(toPlay))
        {
            Debug.LogError("Audio clip \"" + toPlay + "\" not found!", this);
            return;
        }

        var clipArray = _voiceLineMap[toPlay];
        clipArray.PlayRandomOneShot(source);
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
        source.loop = false;
        int clipNumberToPlay = UnityEngine.Random.Range(0,clips.Count);
        source.Stop();
        source.clip = clips[clipNumberToPlay];
        source.Play();
    }

    public void PlayRandomLoop(AudioSource source)
    {
        source.loop = true;
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