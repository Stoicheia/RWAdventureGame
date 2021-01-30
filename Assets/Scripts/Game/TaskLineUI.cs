using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskLineUI : MonoBehaviour
{
    public TextMeshProUGUI taskText;

    public void Strike()
    {
        taskText.fontStyle = FontStyles.Strikethrough;
    }

    public void Unstrike()
    {
        taskText.fontStyle = FontStyles.Normal;
    }
}
