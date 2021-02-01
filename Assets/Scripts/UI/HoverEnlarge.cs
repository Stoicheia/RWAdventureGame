using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEnlarge : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float enlargeBy = 1.2f;
    private Vector3 originalScale;
    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * enlargeBy;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
