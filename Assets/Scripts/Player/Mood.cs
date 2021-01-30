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
        ResetTimeOfLastAction(transform);
    }

    private void OnEnable()
    {
        ClickToMoveController.OnMove += ResetTimeOfLastAction;
    }

    private void OnDisable()
    {
        ClickToMoveController.OnMove -= ResetTimeOfLastAction;
    }

    private void Update()
    {
        CalculateNeutralSound();
    }

    private void ResetTimeOfLastAction(Transform t)
    {
        if(t == transform)
            timeOfLastAction = Time.time;
    }

    private void CalculateNeutralSound()
    {
        if (speaker.IsPlaying()) return;
        
        float timeSinceLastAction = Time.time - timeOfLastAction;
        PlayMaybe(timeSinceLastAction, startHumAfterSeconds,startYawnAfterSeconds, humFrequency, "Humming");
        PlayMaybe(timeSinceLastAction, startYawnAfterSeconds, float.PositiveInfinity, yawnFrequency, "Yawn");
    }

    private void PlayMaybe(float t, float thresholdMin, float thresholdMax, float p, string clip)
    {
        if (t >= thresholdMin && t <= thresholdMax)
        {
            if (UnityEngine.Random.Range(0f, 1.0f) <= Paraboloid01(p))
            {
                speaker.PlayClip(clip);
            }
        }
    }

    public static float Paraboloid01(float t)
    {
        return Mathf.Pow(t, 6);
    }
}
