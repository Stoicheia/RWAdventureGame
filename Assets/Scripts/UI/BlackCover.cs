using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(AudioSource))]
public class BlackCover : MonoBehaviour
{
    public float liftTime = 1;
    public float fadeTime = 0.9f;
    private Image cover;
    private AudioSource source;
    private Inventory inventory;

    private void Awake()
    {
        cover = GetComponent<Image>();
        cover.color = new Color(1, 1, 1, 0);
    }

    private void Start()
    {
        inventory = GlobalStats.instance.PlayerInventory;
        source = FindObjectOfType<ClickToMoveController>().GetComponent<Speaker>().Source;
        StartCoroutine(FadeOutEffect(liftTime));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInEffect(fadeTime));
    }
    
    public void FadeOut()
    {
        StartCoroutine(FadeOutEffect(fadeTime));
    }

    public void StartSequence(InteractibleObject f, Item toRemove, bool removeObject, bool swapObject, List<Spawnable> toSpawn, AudioClip tp, AudioClip tpa, Sprite newPlayerSprite)
    {
        StartCoroutine(Sequence(f, toRemove, removeObject, swapObject, toSpawn,tp, tpa, newPlayerSprite));
    }

    IEnumerator FadeInEffect(float t)
    {
        float init = Time.time;
        while (Time.time <= init + t)
        {
            cover.color = new Color(0, 0, 0,(Time.time-init)/t);
            yield return null;
        }
        cover.color = new Color(0, 0, 0,1);
    }
    
    IEnumerator FadeOutEffect(float t)
    {
        float init = Time.time;
        while (Time.time <= init + t)
        {
            cover.color = new Color(0, 0, 0,1-(Time.time-init)/t);
            yield return null;
        }
        cover.color = new Color(0, 0, 0,0);
    }
    
    IEnumerator Sequence(InteractibleObject f, Item toRemove, bool removeObject, bool swapObject, List<Spawnable> spawning, AudioClip toPlay, AudioClip toPlayAfter, Sprite newPlayerSprite)
    {
        FadeIn();
        yield return new WaitForSeconds(fadeTime + 0.1f);
        if (removeObject) Destroy(f.gameObject);
        if(toRemove!=null) inventory.DeleteItem(toRemove);
        foreach (var spawnable in spawning)
        {
            Transform spawned = Instantiate(spawnable.toSpawn, spawnable.spawnLocation, quaternion.identity);
        }

        if (f.GetComponent<SwappableObject>() != null && swapObject)
        {
            f.GetComponent<SwappableObject>().Swap();
        }

        if (newPlayerSprite != null)
        {
            ClickToMoveController player = FindObjectOfType<ClickToMoveController>();
            player.GetComponentInChildren<SpriteRenderer>().sprite = newPlayerSprite;
        }
        source.PlayOneShot(toPlay);
        yield return new WaitForSeconds(toPlay.length + 0.1f);
        FadeOut();
        source.PlayOneShot(toPlayAfter);
    }
    
}
