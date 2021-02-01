using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Speaker))]
public class FootstepsAudio : MonoBehaviour
{
    private ClickToMoveController controller;
    private InventoryUser inv;
    private RegionStepper stepper;
    private Speaker speaker;
    [SerializeField] private float stepsPerSecond;
    private float secondsPerStep;
    

    private float timeOfLastStep;

    private void Awake()
    {
        speaker = GetComponent<Speaker>();
        stepper = GetComponent<RegionStepper>();
        secondsPerStep = 1 / stepsPerSecond;
        timeOfLastStep = Time.time;
    }

    private void Start()
    {
        controller = transform.parent.GetComponent<ClickToMoveController>();
        inv = controller.GetComponent<InventoryUser>();
        if (controller == null) Debug.LogError("PLease set this object as a child of the player controller", this);
    }

    private void Update()
    {
        if (controller.EnRoute)
        {
            if (Time.time > timeOfLastStep + secondsPerStep)
            {
                if(!inv.hasShoes)
                    speaker.PlayOneShot("Barefoot");
                else if (stepper.currentRegion != null)
                {
                    if (stepper.currentRegion.regionType.regionName == "Cabin")
                        speaker.PlayOneShot("Wood");
                }
                else
                {
                    speaker.PlayOneShot("Grass");
                }
                timeOfLastStep = Time.time;
            }
        }
    }
}
