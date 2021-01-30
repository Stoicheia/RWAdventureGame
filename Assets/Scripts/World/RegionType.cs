using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Region Type", menuName = "Region Type")]
public class RegionType : ScriptableObject
{
    public string regionName;
    public AudioClip ambienceAudio;
    public AudioClipArray musicAudio;
}
