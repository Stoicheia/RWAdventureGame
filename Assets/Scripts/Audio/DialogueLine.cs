using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

[CreateAssetMenu(fileName = "New Voiceline", menuName = "Voiceline")]
public class DialogueLine : ScriptableObject
{
    [SerializeField] private AudioClip audio;
    [TextArea(3,8)]
    [SerializeField] private string subtitles;

    #region Properties

    public AudioClip Audio
    {
        get => audio;
        private set => audio = value;
    }

    public string Subtitles
    {
        get => subtitles;
        private set => subtitles = value;
    }

    #endregion

    public void PlayAudio(AudioSource source)
    {
        source.PlayOneShot(audio);
    }

    public void ShowDialogue(TextMeshProUGUI textField)
    {
        textField.text = subtitles;
    }
}
