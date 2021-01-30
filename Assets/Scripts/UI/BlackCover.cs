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
    private Inventory inventory;

    private void Awake()
    {
        cover = GetComponent<Image>();
        cover.color = new Color(1, 1, 1, 0);
    }

    private void Start()
    {
        inventory = GlobalStats.instance.PlayerInventory;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInEffect(fadeTime));
    }
    
    public void FadeOut()
    {
        StartCoroutine(FadeOutEffect(fadeTime));
    }

    public void StartSequence(InteractibleObject f, Item toRemove, bool removeObject, List<Spawnable> toSpawn)
    {
        StartCoroutine(Sequence(f, toRemove, removeObject, toSpawn));
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
    
    IEnumerator Sequence(InteractibleObject f, Item toRemove, bool removeObject, List<Spawnable> spawning)
    {
        FadeIn();
        yield return new WaitForSeconds(fadeTime + 0.1f);
        if (removeObject) Destroy(f.gameObject);
        if(toRemove!=null) inventory.DeleteItem(toRemove);
        foreach (var spawnable in spawning)
        {
            Transform spawned = Instantiate(spawnable.toSpawn, spawnable.spawnLocation);
        }
        yield return new WaitForSeconds(fadeTime + 0.1f);
        FadeOut();
    }
}
