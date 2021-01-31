using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "Dialogue Sequence")]
public class DialogueSequence : MonoBehaviour
{
    [SerializeField] public List<DialogueLine> lines;
}
