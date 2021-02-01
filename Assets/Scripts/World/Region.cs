using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Region : MonoBehaviour
{
    private const float REGION_CHANGE_COOLDOWN = 2;
    public delegate void RegionChangeAction(Region r);

    public static event RegionChangeAction OnRegionChange;
    
    private Collider2D col;
    private ClickToMoveController activePlayer;

    public RegionType regionType;

    public static Region ActiveRegion;
    private static float lastChange;
    
    private void Start()
    {
        lastChange = Time.time;
        col = GetComponent<Collider2D>();
        activePlayer = FindObjectOfType<ClickToMoveController>();
        ActiveRegion = FindObjectOfType<RegionalAudioManager>().defaultRegion;
    }

    private void Update()
    {
    }

    public static void UpdateActiveRegion(Region r)
    {
        ActiveRegion = r;
        if (Time.time - lastChange >= REGION_CHANGE_COOLDOWN)
        {
            OnRegionChange?.Invoke(r);
            lastChange = Time.time;
        }
    }
}
