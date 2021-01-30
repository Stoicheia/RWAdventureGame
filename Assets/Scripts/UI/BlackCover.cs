using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BlackCover : MonoBehaviour
{
    public float fadeTime = 0.9f;
    private Image cover;

    private void Awake()
    {
        cover = GetComponent<Image>();
        cover.color = new Color(1, 1, 1, 0);
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInEffect(fadeTime));
    }
    
    public void FadeOut()
    {
        StartCoroutine(FadeOutEffect(fadeTime));
    }

    IEnumerator FadeInEffect(float t)
    {
        float init = Time.time;
        while (Time.time <= init + t)
        {
            cover.color = new Color(1, 1, 1,(Time.time-init)/t);
            yield return null;
        }
        cover.color = new Color(1, 1, 1,1);
    }
    
    IEnumerator FadeOutEffect(float t)
    {
        float init = Time.time;
        while (Time.time <= init + t)
        {
            cover.color = new Color(1, 1, 1,1-(Time.time-init)/t);
            yield return null;
        }
        cover.color = new Color(1, 1, 1,0);
    }
}
