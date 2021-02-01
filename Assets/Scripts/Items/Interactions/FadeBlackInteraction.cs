using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fade Interaction", menuName = "Interactions/Fade Interaction")]
public class FadeBlackInteraction : ItemObjectInteraction
{
    private BlackCover cover;

    [SerializeField] private Item itemToRemove;
    [SerializeField] private bool removeInteractedObject;
    [SerializeField] private List<Spawnable> objectsToSpawn;
    [SerializeField] private bool swapObject;
    [SerializeField] public AudioClip toPlay;
    [SerializeField] public AudioClip toPlayAfter;
    [SerializeField] private bool giveShoes;

    public override void Act(InteractibleObject from)
    {
        cover = FindObjectOfType<BlackCover>();
        if (from == null) removeInteractedObject = false;
        cover.StartSequence(from, itemToRemove, removeInteractedObject, swapObject, objectsToSpawn, toPlay, toPlayAfter, giveShoes);
    }

    
}

[System.Serializable]
public class Spawnable
{
    public Transform toSpawn;
    public Vector3 spawnLocation;
}
