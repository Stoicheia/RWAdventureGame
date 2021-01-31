using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class InteractibleObject : MonoBehaviour
{
    public string objectName;
    private InventoryUser player;

    private int timesInteracted;
    [SerializeField] private DialogueSequence firstTimeDialogue;
    [SerializeField] private DialogueSequence otherTimeDialogue;
    [SerializeField] private AudioClip sfx;

    private AudioSource objectAudio;

    private Camera camera;

    private void Awake()
    {
        timesInteracted = 0;
        objectAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InventoryUser[] players = FindObjectsOfType<InventoryUser>();
        if (players.Length>1) Debug.LogWarning("Multiple Players Foudn!");
        player = FindObjectsOfType<InventoryUser>()[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && (transform.position-player.transform.position).magnitude <= player.interactionRadius)
        {
            DetectHit();
        }
    }

    void DetectHit()
    {
        print("usage detected!");
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        InteractibleObject hitObj;
        foreach (var hit in hits)
        {
            hitObj = hit.collider.GetComponent<InteractibleObject>();
            if (hitObj == this)
            {
                print("up");
                ItemUI selectedItemUI = ItemDragHandler.from;
                if (selectedItemUI == null)
                {
                    InteractWithObject();
                    return;
                }
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

    public virtual void InteractWithObject()
    {
        
        objectAudio.PlayOneShot(sfx);
        
        
        timesInteracted++;
    }
    
}
