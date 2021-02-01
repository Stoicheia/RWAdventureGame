using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalStats : MonoBehaviour
{
    public static GlobalStats instance;
    [SerializeField] private Inventory playerInventory;

    public Inventory PlayerInventory
    {
        get => playerInventory;
        set 
        { 
            playerInventory = value;
            Debug.LogWarning("Player inventory changed", this);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToScene(int s)
    {
        
    }
}
