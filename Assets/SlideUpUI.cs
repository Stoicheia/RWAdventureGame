using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideUpUI : MonoBehaviour
{
    [SerializeField] private float slideDistance;
    [SerializeField] private float slideDuration;
    [SerializeField] private Button slideButton;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private bool up;
    private bool enRoute;

    private void Awake()
    {
        up = false;
        originalPosition = transform.localPosition;
        targetPosition = new Vector3(originalPosition.x, originalPosition.y + slideDistance, originalPosition.z);
    }

    private void OnEnable()
    {
        slideButton.onClick.AddListener(Slide);
    }

    private void OnDisable()
    {
        slideButton.onClick.RemoveListener(Slide);
    }

    private void Start()
    {
       
    }
    void Slide()
    {
        if (enRoute) return;
        if(up) SlideBack();
        else SlideUp();
    }

    void SlideUp()
    {
        StartCoroutine(SlideSequence(slideDuration,slideDistance,originalPosition,targetPosition));
        up = true;
    }

    void SlideBack()
    {
        StartCoroutine(SlideSequence(slideDuration, slideDistance, targetPosition, originalPosition));
        up = false;
    }

    IEnumerator SlideSequence(float t, float d, Vector3 u, Vector3 v)
    {
        enRoute = true;
        float init = Time.time;
        while (Time.time <= init + t)
        {
            transform.localPosition = Vector3.Lerp(u,v,CompactSigmoid((Time.time-init)/t));
            yield return null;
        }

        enRoute = false;
    }

    static float CompactSigmoid(float f)
    {
        return f;
    }
}
