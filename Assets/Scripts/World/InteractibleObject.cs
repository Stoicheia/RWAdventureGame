using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]
public class InteractibleObject : MonoBehaviour
{
    public string objectName;



    private Camera camera;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            DetectHit();
        }
    }

    void DetectHit()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        InteractibleObject hitObj;
        foreach (var hit in hits)
        {
            hitObj = hit.collider.GetComponent<InteractibleObject>();
            if (hitObj == this)
            {
                print("up");
                ItemUI selectedItemUI = ItemDragHandler.from;
                if (selectedItemUI == null) return;
                Item selectedItem = selectedItemUI.DisplayedItem.item;
                if (selectedItem == null) return;

                foreach (var interactible in selectedItem.ItemInteractions)
                {
                    if (interactible.interactibleObject.objectName == objectName)
                    {
                        foreach(var interaction in interactible.interactions)
                            interaction.Act(this);
                    }
                }

                return;
            }
        }
    }
    
}
