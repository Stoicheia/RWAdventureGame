using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogueSubtitles : MonoBehaviour
{
    private TextMeshProUGUI tmpro;
    private Coroutine tfe;
    private Coroutine tde;

    private void Awake()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
        tmpro.text = "";
    }

    public void SetText(string s)
    {
        if(tfe!=null) StopCoroutine(tfe);
        tfe = StartCoroutine(TextFadeEffect(s, 0.85f));
    }

    public void DeleteText()
    {
        StartCoroutine(TextDieEffect(0.85f));
    }

    IEnumerator TextFadeEffect(string text, float duration)
    {
        tmpro.text = text;
        float t = Time.time;
        while (Time.time <= t + duration)
        {
            tmpro.color = new Color(0,0,0,(Time.time-t)/(duration));
            yield return null;
        }
        tmpro.color = new Color(0,0,0,1);
    }
    
    IEnumerator TextDieEffect(float duration)
    {
        float t = Time.time;
        while (Time.time <= t + duration)
        {
            tmpro.color = new Color(0,0,0,1-(Time.time-t)/(duration));
            yield return null;
        }
        tmpro.color = new Color(0,0,0,0);
        tmpro.text = "";
    }
}
