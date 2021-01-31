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
    protected InventoryUser player;
    protected Collider2D playerCol;
    protected Collider2D myCol;

    protected int timesInteracted;
    [SerializeField] private DialogueSequence firstTimeDialogue;
    [SerializeField] private Item criticalItem;
    [SerializeField] private DialogueSequence noItemFirstTimeDialogue;
    [SerializeField] private DialogueSequence otherTimeDialogue;
    [SerializeField] private AudioClip sfx;

    private AudioSource objectAudio;
    private DialogueSystem dialogueSource;

    private Camera camera;

    private void Awake()
    {
        timesInteracted = 0;
        objectAudio = GetComponent<AudioSource>();
        objectAudio.spatialBlend = 1;
        myCol = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        InventoryUser[] players = FindObjectsOfType<InventoryUser>();
        if (players.Length>1) Debug.LogWarning("Multiple Players Foudn!");
        player = FindObjectsOfType<InventoryUser>()[0];
        playerCol = player.GetComponent<Collider2D>();
        dialogueSource = FindObjectOfType<DialogueSystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && myCol.Distance(playerCol).distance <= player.interactionRadius)
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
                if (selectedItemUI == null)
                {
                    InteractWithObject();
                    return;
                }
                Item selectedItem = selectedItemUI.DisplayedItem.item;
                if (selectedItem == null) return;

                if (selectedItem is ItemCombAct)
                {
                    print("here");
                    ItemCombAct ica = (ItemCombAct) selectedItem;
                    if (GlobalStats.instance.PlayerInventory.HasItem(ica.complement))
                    {
                        foreach (var interactible in ica.successfulItemIneraction)
                        {
                            if (interactible.interactibleObject.objectName == objectName)
                            {
                                foreach(var interaction in interactible.interactions)
                                    interaction.Act(this);
                            }
                        }
                    }
                    else
                    {
                        foreach (var interactible in ica.neutralItemInteraction)
                        {
                            if (interactible.interactibleObject.objectName == objectName)
                            {
                                foreach(var interaction in interactible.interactions)
                                    interaction.Act(this);
                            }
                        }
                    }

                    return;
                }

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
        print("s");
        if(sfx!=null)
            objectAudio.PlayOneShot(sfx);

        DialogueSequence dialogueToUse = timesInteracted <= 0 ? firstTimeDialogue : otherTimeDialogue;

        if (timesInteracted <= 0 && !GlobalStats.instance.PlayerInventory.HasItem(criticalItem))
        {
            dialogueToUse = noItemFirstTimeDialogue;
        }
        
        if(dialogueToUse!=null)
            dialogueSource.SetDialogue(dialogueToUse);

        timesInteracted++;
    }
    
}
