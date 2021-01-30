using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Region : MonoBehaviour
{
    public delegate void RegionChangeAction(Region r);

    public static event RegionChangeAction OnRegionChange;
    
    private Collider2D col;
    private ClickToMoveController activePlayer;

    public RegionType regionType;
    
    public static Region ActiveRegion;
    
    private void Start()
    {
        col = GetComponent<Collider2D>();
        activePlayer = FindObjectOfType<ClickToMoveController>();
    }

    private void Update()
    {
        
    }

    public static void UpdateActiveRegion(Region r)
    {
        ActiveRegion = r;
        OnRegionChange?.Invoke(r);
    }
}
