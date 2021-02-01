using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ShoeHideUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite showNeutral;
    public Sprite showActive;
    public Sprite hideNeutral;
    public Sprite hideActive;

    public AudioClip showSound;
    public AudioClip hideSound;

    private Image target;
    private bool shown;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        target = GetComponentsInChildren<Image>()[1];
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        target.sprite = shown ? hideActive : showActive;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        target.sprite = shown ? hideNeutral : showNeutral;
    }

    public void OnShow()
    {
        shown = true;
        source.PlayOneShot(showSound);
        target.sprite = hideNeutral;
    }

    public void OnHide()
    {
        shown = false;
        source.PlayOneShot(hideSound);
        target.sprite = showNeutral;
    }
}
