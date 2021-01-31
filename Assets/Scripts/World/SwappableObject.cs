using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SwappableObject : InteractibleObject
{
    public Transform active;
    public Transform inactive;

    protected override void Start()
    {
        base.Start();
        
        active.gameObject.SetActive(false);
        inactive.gameObject.SetActive(true);
    }

    public void Swap()
    {
        active.gameObject.SetActive(true);
        inactive.gameObject.SetActive(false);
    }
}
