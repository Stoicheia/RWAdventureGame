using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Region : MonoBehaviour
{
    private Collider2D col;
    private ClickToMoveController activePlayer;

    public static Region ActiveRegion;
    
    private void Start()
    {
        col = GetComponent<Collider2D>();
        activePlayer = FindObjectOfType<ClickToMoveController>();
    }

    private void Update()
    {
        if (col.bounds.Contains(activePlayer.transform.position))
        {
            ActiveRegion = this;
        }
    }
}
