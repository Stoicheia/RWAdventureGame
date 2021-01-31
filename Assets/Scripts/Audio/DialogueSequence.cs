using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "Dialogue Sequence")]
public class DialogueSequence : ScriptableObject
{
    [SerializeField] public List<DialogueLine> lines;
    public bool memory;
    public DialogueSequence nextDialogue;
}
