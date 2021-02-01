using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MemoryNoises : MonoBehaviour
{
    private AudioSource source;
    public AudioClip loop;
    public AudioClip begin;
    public AudioClip end;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play()
    {
        source.loop = true;
        source.clip = loop;
        source.Play();
    }

    public void Begin()
    {
        source.PlayOneShot(begin);
    }

    public void End()
    {
        bool flag = false;
        if (source.isPlaying)
        {
            flag = true;
            source.Stop();
        }
        if(flag)
            source.PlayOneShot(end);
    }
}
