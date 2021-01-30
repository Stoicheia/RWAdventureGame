using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractibleObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string objectName;
    public void OnDrop(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ItemUI selectedItemUI = ItemDragHandler.from;
        Item selectedItem = selectedItemUI.DisplayedItem.item;

        foreach (var interactible in selectedItem.ItemInteractions)
        {
            if (interactible.interactibleObject.objectName == objectName)
            {
                foreach(var interaction in interactible.interactions)
                    interaction.Act(this);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
