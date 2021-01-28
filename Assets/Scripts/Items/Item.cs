using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public const string GENERIC_ITEM_NAME = "New Item";
    public const string GENERIC_DESCRIPTION_TEXT = "This seems interesting...";

    private int itemID;
    [SerializeField] private string itemName;
    [TextArea(2,8)]
    [SerializeField] private string description;
    [SerializeField] private Dictionary<Item, ItemInteraction> itemInteractions;
    [SerializeField] private AudioClip narrationAudio;
    [SerializeField] private SpriteRenderer inventorySprite;
    [SerializeField] private SpriteRenderer worldSprite;

    public void PlayNarration(AudioSource source)
    {
        source.clip = narrationAudio;
        source.loop = false;
        source.Play();
    }

    public void Act(Item item)
    {
        Transform popup = Instantiate(itemInteractions[item].popupUI);
        //TODO: popup.parent = the canvas
        //TODO: do all the effects
    }

    #region Properties
    public int ItemID
    {
        get => itemID;
        set { itemID = value; Debug.Log(($"{itemName}'s item ID is now {itemID}")); }
    }
    
    public string ItemName
    {
        get => itemName == "" ? GENERIC_ITEM_NAME : itemName;
        private set => itemName = value;
    }
    
    public string Description
    {
        get => description == "" ? GENERIC_DESCRIPTION_TEXT : description;
        private set => description = value;
    }

    public Dictionary<Item, ItemInteraction> ItemInteractions
    {
        get => itemInteractions;
        private set => itemInteractions = value;
    }

    public AudioClip NarrationAudio
    {
        get => narrationAudio;
        private set => narrationAudio = value;
    }

    public Sprite InventorySprite
    {
        get => inventorySprite.sprite;
        private set => inventorySprite.sprite = value;
    }

    public Sprite WorldSprite
    {
        get => worldSprite.sprite;
        private set => worldSprite.sprite = value;
    }
    #endregion

}


