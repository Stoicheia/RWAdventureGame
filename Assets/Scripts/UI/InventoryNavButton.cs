using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryNavButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool Held;
    public bool Hovered;

    private void OnEnable()
    {
        Held = false;
        Hovered = false;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Held = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Held = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hovered = false;
    }
}
