using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fade Interaction", menuName = "Interactions/Fade Interaction")]
public class FadeBlackInteraction : ItemObjectInteraction
{
    private BlackCover cover;

    [SerializeField] private Item itemToRemove;
    [SerializeField] private bool removeInteractedObject;
    [SerializeField] private List<Spawnable> objectsToSpawn;
    [SerializeField] private AudioClip toPlay;

    private void Awake()
    {
        cover = FindObjectOfType<BlackCover>();
    }
    
    public override void Act(InteractibleObject from)
    {
        cover.StartSequence(from, itemToRemove, removeInteractedObject, objectsToSpawn, toPlay);
    }

    
}

[System.Serializable]
public class Spawnable
{
    public Transform toSpawn;
    public Vector3 spawnLocation;
}
