using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShoeHideUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite showNeutral;
    public Sprite showActive;
    public Sprite hideNeutral;
    public Sprite hideActive;

    private Image target;
    private bool shown;

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
        target.sprite = hideNeutral;
    }

    public void OnHide()
    {
        shown = false;
        target.sprite = showNeutral;
    }
}
