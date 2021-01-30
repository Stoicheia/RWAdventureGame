using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Mood : MonoBehaviour
{
    private Speaker speaker;

    private float timeOfLastAction;
    [SerializeField] private float startHumAfterSeconds;
    [SerializeField] private float startYawnAfterSeconds;
    [Range(0,1)]
    [SerializeField] private float humFrequency;
    [Range(0,1)]
    [SerializeField] private float yawnFrequency;

    private void Awake()
    {
        speaker = GetComponent<Speaker>();
        if(speaker==null) Debug.LogWarning("This entity cannot make sound.", this);
        ResetTimeOfLastAction();
    }

    private void OnEnable()
    {
        ClickToMoveController clickToMoveController = GetComponentInChildren<ClickToMoveController>();
        clickToMoveController.OnNavigationStarted += navigationEventCallback;
        clickToMoveController.OnNavigationFailed += navigationEventCallback;
    }

    private void OnDisable()
    {
        ClickToMoveController clickToMoveController = GetComponentInChildren<ClickToMoveController>();

        clickToMoveController.OnNavigationStarted -= navigationEventCallback;
        clickToMoveController.OnNavigationFailed -= navigationEventCallback;
    }

    private void Update()
    {
        CalculateNeutralSound();
    }

    public void InterruptMood()
    {
        ResetTimeOfLastAction();
    }

    private void navigationEventCallback(Transform t, Vector3 wp)
    {
        ResetTimeOfLastAction();
    }

    private void ResetTimeOfLastAction()
    {
        timeOfLastAction = Time.time;
    }

    private void CalculateNeutralSound()
    {
        if (speaker.IsPlaying()) return;
        
        float timeSinceLastAction = Time.time - timeOfLastAction;
        if (!PlayMaybe(timeSinceLastAction, startHumAfterSeconds,startYawnAfterSeconds, humFrequency, "Humming"))
            PlayMaybe(timeSinceLastAction, startYawnAfterSeconds, float.PositiveInfinity, yawnFrequency, "Yawn");
    }

    private bool PlayMaybe(float t, float thresholdMin, float thresholdMax, float p, string clip)
    {
        if (t >= thresholdMin && t <= thresholdMax)
        {
            if (UnityEngine.Random.Range(0f, 1.0f) <= Paraboloid01(p))
            {
                speaker.PlayClip(clip);
                return true;
            }
        }

        return false;
    }

    public static float Paraboloid01(float t)
    {
        return Mathf.Pow(t, 6);
    }
}
