using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "EENNND", menuName = "New End Interaction")]
public class EndInteraction : ItemObjectInteraction
{
    private BlackCover cover;
    public float cooldown;
    public float time;
    public override void Act(InteractibleObject @from)
    {
        cover = FindObjectOfType<BlackCover>();
        cover.End(time, cooldown);
    }
}
