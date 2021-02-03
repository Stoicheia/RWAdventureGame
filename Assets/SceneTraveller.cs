using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneTraveller : MonoBehaviour
{
    
    public void GoToScene(int s)
    {
        GlobalStats.instance.PlayerInventory.DeleteAll();
        SceneManager.LoadScene(s);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
