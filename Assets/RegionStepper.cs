using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class RegionStepper : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        Region region = other.gameObject.GetComponent<Region>();
        // print(region.name);
        if (region != null && region!=Region.ActiveRegion)
        {
            print("hayaite");
            Region.UpdateActiveRegion(region);
        }
    }
}
