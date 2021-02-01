using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BlackNarration : MonoBehaviour
{
    public float fadeTime = 1.2f;
    [SerializeField] private DialogueSequence blackSequence;
    [SerializeField] private Image blackPanel;

    private DialogueSystem dialogueSource;

    private void Start()
    {
        dialogueSource = FindObjectOfType<DialogueSystem>();
        blackPanel.gameObject.SetActive(false);
    }

    public void Act()
    {
        StartCoroutine(Sequence(fadeTime));
    }

    IEnumerator Sequence(float t)
    {
        StartCoroutine(FadePanelIn(t));
        yield return new WaitForSeconds(t + 0.2f);
        dialogueSource.SetDialogue(blackSequence);
        yield return new WaitForSeconds(blackSequence.lines[0].Audio.length);
        GlobalStats.instance.GoToScene(1);
    }

    IEnumerator FadePanelIn(float t)
    {
        blackPanel.gameObject.SetActive(true);
        float init = Time.time;
        while (Time.time <= init + t)
        {
            blackPanel.color = new Color(0, 0, 0,(Time.time-init)/t);
            yield return null;
        }
        blackPanel.color = new Color(0, 0, 0,1);
    }
}
