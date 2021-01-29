using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryNavigationUI : MonoBehaviour
{
    [Range(0,0.99f)]
    public float scrollIntensityClick;

    [Range(0.01f, 9.99f)] 
    public float scrollIntensityHold;

    public InventoryNavButton right;
    public InventoryNavButton left;
    public ScrollRect scroller;

    public void LeftNavClicked()
    {
        scroller.horizontalNormalizedPosition = Mathf.Max(0, scroller.horizontalNormalizedPosition-scrollIntensityClick);
    }

    public void RightNavClicked()
    {
        scroller.horizontalNormalizedPosition = Mathf.Min(1, scroller.horizontalNormalizedPosition+scrollIntensityClick);
    }

    private void Update()
    {
        if(ItemDragHandler.dragging)
            CalculateButtonHover();
        else
            CalculateButtonClick();
    }

    private void CalculateButtonClick()
    {
        if (right.Held)
        {
            scroller.horizontalNormalizedPosition = Mathf.Min(1, scroller.horizontalNormalizedPosition+scrollIntensityHold*Time.deltaTime);
        }
        if (left.Held)
        {
            scroller.horizontalNormalizedPosition = Mathf.Max(0, scroller.horizontalNormalizedPosition-scrollIntensityHold*Time.deltaTime);
        }
    }

    private void CalculateButtonHover()
    {
        if (right.Hovered)
        {
            scroller.horizontalNormalizedPosition = Mathf.Min(1, scroller.horizontalNormalizedPosition+scrollIntensityHold*Time.deltaTime);
        }
        if (left.Hovered)
        {
            scroller.horizontalNormalizedPosition = Mathf.Max(0, scroller.horizontalNormalizedPosition-scrollIntensityHold*Time.deltaTime);
        }
    }
}
