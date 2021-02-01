using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
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

    public Image endButton;
    public Image endPicture;
    public AudioClip endAudio;
    public DialogueSequence endDialogue;

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

    public void StartSequence(InteractibleObject f, Item toRemove, bool removeObject, bool swapObject, List<Spawnable> toSpawn, AudioClip tp, AudioClip tpa, bool giveShoes)
    {
        StartCoroutine(Sequence(f, toRemove, removeObject, swapObject, toSpawn,tp, tpa, giveShoes));
    }

    public void End(float t, float c)
    {
        StartCoroutine(FadeAllInEffect(t,c));
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
    
    IEnumerator Sequence(InteractibleObject f, Item toRemove, bool removeObject, bool swapObject, List<Spawnable> spawning, AudioClip toPlay, AudioClip toPlayAfter, bool giveShoes)
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

        if (giveShoes)
        {
            InventoryUser player = FindObjectOfType<InventoryUser>();
            player.hasShoes = true;
        }
        source.PlayOneShot(toPlay);
        yield return new WaitForSeconds(toPlay.length + 0.1f);
        FadeOut();
        source.PlayOneShot(toPlayAfter);
    }


    IEnumerator FadeAllInEffect(float t, float c)
    {
        yield return new WaitForSeconds(c);
        source.clip = endAudio;
        source.Play();
        endButton.gameObject.SetActive(true);
        endButton.enabled = false;
        endPicture.gameObject.SetActive(true);
        float init = Time.time;
        while (Time.time <= init + t)
        {
            cover.color = new Color(0, 0, 0,(Time.time-init)/t);
            endButton.color = new Color(1, 1, 1,(Time.time-init)/t);
            endButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1,(Time.time-init)/t);
            endPicture.color = new Color(1, 1, 1,(Time.time-init)/t);
            yield return null;
        }
        FindObjectOfType<DialogueSystem>().SetDialogue(endDialogue);
        endButton.enabled = true;
        cover.color = new Color(0, 0, 0,1);
    }
    
}
