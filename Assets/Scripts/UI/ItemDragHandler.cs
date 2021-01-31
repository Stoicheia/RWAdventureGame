using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static ItemUI from;
    public static bool dragging;
    
    private ItemUI but;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        but = transform.parent.GetComponent<ItemUI>();
        if(but==null) Debug.LogError("No ItemUI found", this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (but.DisplayedItem == null) return;
        if (but.DisplayedItem.item == null) return;
        dragging = true;
        transform.SetParent(FindObjectOfType<Canvas>().transform, true);
        from = but;
        canvasGroup.alpha = .65f;
        canvasGroup.blocksRaycasts = false;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(NextFrameAction());
    }

    IEnumerator NextFrameAction()
    {
        yield return null;
        yield return null;
        EndDrag();
    }

    void EndDrag()
    {
        dragging = false;
        if (but != null)
            transform.SetParent(but.transform);
        else
            Debug.LogWarning("Item probably ended up in the wrong place...", this);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        from = null;
        transform.localPosition = Vector3.zero;
    }
}
