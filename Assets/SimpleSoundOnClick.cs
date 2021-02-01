using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent((typeof(AudioSource)))]
public class SimpleSoundOnClick : MonoBehaviour, IPointerClickHandler
{
    private AudioSource source;
    public AudioClip clipOnClick;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        source.PlayOneShot(clipOnClick);
    }
}
