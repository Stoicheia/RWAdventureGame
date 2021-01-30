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
    private Speaker speaker;
    [SerializeField] private float stepsPerSecond;
    private float secondsPerStep;
    

    private float timeOfLastStep;

    private void Awake()
    {
        speaker = GetComponent<Speaker>();
        secondsPerStep = 1 / stepsPerSecond;
        timeOfLastStep = Time.time;
    }

    private void Start()
    {
        controller = transform.parent.GetComponent<ClickToMoveController>();
        if (controller == null) Debug.LogError("PLease set this object as a child of the player controller", this);
    }

    private void Update()
    {
        if (controller.EnRoute)
        {
            if (Time.time > timeOfLastStep + secondsPerStep)
            {
                speaker.PlayOneShot("Barefoot");
                timeOfLastStep = Time.time;
            }
        }
    }
}
