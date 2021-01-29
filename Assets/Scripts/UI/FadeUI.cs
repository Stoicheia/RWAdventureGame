using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FadeUI : MonoBehaviour
{
    private TextMeshProUGUI tmpro;
    public float fadeTime;

    private IEnumerator currentFadeSequence;
    private void Awake()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
        if (tmpro == null)
        {
            Debug.LogError("No Text Field Found!", this);
        }
    }

    public void SetText(string s)
    {
        if(currentFadeSequence!=null)
            StopCoroutine(currentFadeSequence);
        tmpro.text = s;
        currentFadeSequence = FadeEffectSequence(fadeTime);
        StartCoroutine(currentFadeSequence);
    }

    IEnumerator FadeEffectSequence(float s)
    {
        Color originalColor = tmpro.color;
        float t = Time.time;
        while (Time.time < t + s)
        {
            tmpro.color = new Color(originalColor.r, originalColor.g, originalColor.b, (Time.time - t) / s);
            yield return null;
        }
    }
}
