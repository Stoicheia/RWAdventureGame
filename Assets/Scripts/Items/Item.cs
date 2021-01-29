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

    [SerializeField] private List<Sprite> descriptionDrawings;
    [SerializeField] private List<ItemInteractionPair> itemInteractions;
    [SerializeField] private DialogueLine narrationAudio;
    [SerializeField] private SpriteRenderer inventorySprite;
    [SerializeField] private SpriteRenderer worldSprite;
    [SerializeField] private bool consumable;

    public void PlayNarration(AudioSource source)
    {
        source.clip = narrationAudio.Audio;
        source.loop = false;
        source.Play();
    }

    public void Act(Item item)
    {
        if (item == null) return;
        if(!ActOneWay(item))
            item.ActOneWay(this);
    }

    bool ActOneWay(Item item)
    {
        foreach (ItemInteractionPair pair in itemInteractions)
        {
            if (pair.item.ItemID == item.ItemID)
            {
                Transform popup = Instantiate(pair.interaction.popupUI, FindObjectOfType<Canvas>().transform, true);
                RectTransform rect = popup.GetComponent<RectTransform>();
                rect.offsetMax = new Vector2(0, 0);
                rect.offsetMin = new Vector2(0, 0);


                //TODO: do all the effects
                return true;
            }
        }

        return false;
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

    public List<Sprite> DescriptionDrawings
    {
        get => descriptionDrawings;
        private set => descriptionDrawings = value;
    }

    public List<ItemInteractionPair> ItemInteractions
    {
        get => itemInteractions;
        private set => itemInteractions = value;
    }

    public DialogueLine NarrationAudio
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

    public bool Consumable
    {
        get => consumable;
        private set => consumable = value;
    }
    

    #endregion

}

[System.Serializable]
public class ItemInteractionPair
{
    public Item item;
    public ItemInteraction interaction;
}