using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Audio;
public class RegionalAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioSource ambientSourceCross;
    private AudioSource currentAmbientSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float fadeTime;
        
    private ClickToMoveController player;
    [SerializeField] private List<Region> regions;

    [SerializeField] private float playIntervalMin;
    [SerializeField] private float playIntervalMax;

    private float lastPlay;
    private float nextPlay;
    private Region activeRegion;

    private Coroutine currentFadeOut;
    private Coroutine currentFadeIn;

    private void Start()
    {
        player = FindObjectOfType<ClickToMoveController>();
        if(player==null) Debug.LogError("No player found.");
        lastPlay = Time.time;
        nextPlay = lastPlay + UnityEngine.Random.Range(playIntervalMin, playIntervalMax);
        currentAmbientSource = ambientSource;
    }

    private void OnEnable()
    {
        Region.OnRegionChange += StartNewAudio;
    }

    private void OnDisable()
    {
        Region.OnRegionChange -= StartNewAudio;
    }

    public void StartNewAudio(Region r)
    {
        activeRegion = r;
        
        //if(currentFadeOut!=null) StopCoroutine(currentFadeOut);
        currentFadeOut = StartCoroutine(FadeOutEffect(currentAmbientSource, fadeTime));
        SwitchAmbientSource();
        currentAmbientSource.clip = activeRegion.regionType.ambienceAudio;
        //if(currentFadeIn!=null) StopCoroutine(currentFadeOut);
        currentAmbientSource.Play();
        currentFadeIn = StartCoroutine(FadeInEffect(currentAmbientSource, fadeTime));
    }

    private void Update()
    {
        print(currentAmbientSource.name);
        if (!(Time.time >= nextPlay)) return;
        if(activeRegion!=null)
            activeRegion.regionType.musicAudio.PlayRandom(musicSource);
        lastPlay = Time.time;
        nextPlay = lastPlay + UnityEngine.Random.Range(playIntervalMin, playIntervalMax);
    }

    IEnumerator FadeInEffect(AudioSource source, float t)
    {
        source.volume = 0;
        float st = Time.time;
        while (Time.time - st <= t)
        {
            source.volume = (Time.time-st)/t;
            yield return null;
        }

        source.volume = 1;
    }

    IEnumerator FadeOutEffect(AudioSource source, float t)
    {
        source.volume = 1;
        float st = Time.time;
        while (Time.time - st <= t)
        {
            source.volume = 1-(Time.time-st)/t;
            yield return null;
        }

        source.Stop();
    }

    private void SwitchAmbientSource()
    {
        currentAmbientSource = currentAmbientSource == ambientSource ? ambientSourceCross : ambientSource;
    }
}
