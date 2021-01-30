using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    public void Close()
    {
        Destroy(gameObject);
    }
}
