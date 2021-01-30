using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class RegionalAudioManager : MonoBehaviour
{
    private ClickToMoveController player;
    [SerializeField] private List<Region> regions;

    private void Start()
    {
        player = FindObjectOfType<ClickToMoveController>();
        if(player==null) Debug.LogError("No player found.");
    }

    private void Update()
    {

    }

    private void SetActiveRegion()
    {
        
    }
}
