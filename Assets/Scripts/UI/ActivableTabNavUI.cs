using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActivableTabNavUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite standardSprite;
    public Sprite hoveredSprite;
    public Sprite selectedSprite;

    private Image img;

    private bool active;

    private void Awake()
    {
        active = false;
    }

    private void Start()
    {
        img = GetComponentsInChildren<Image>()[1];
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (active) return;
        img.sprite = hoveredSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (active) return;
        img.sprite = standardSprite;
    }

    public void Activate()
    {
        active = true;
        img.sprite = selectedSprite;
    }

    public void Deactivate()
    {
        active = false;
        img.sprite = standardSprite;
    }
}
