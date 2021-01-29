using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookNavigationUI : MonoBehaviour
{
    [SerializeField] private List<ButtonMenuPair> buttonsAndMenus;
    [SerializeField] private Transform startingMenu;
    
    private Transform activeMenu;

    private void OnEnable()
    {
        OpenMenu(startingMenu);
        foreach (var bmp in buttonsAndMenus)
        {
            bmp.button.onClick.AddListener(delegate { OpenMenu(bmp.menu); });
        }
    }

    private void OnDisable()
    {
        foreach (var bmp in buttonsAndMenus)
        {
            bmp.button.onClick.RemoveAllListeners();
        }
    }

    void OpenMenu(Transform menuToOpen)
    {
        if(activeMenu!=null) activeMenu.gameObject.SetActive(false);
        menuToOpen.gameObject.SetActive(true);
        activeMenu = menuToOpen;
    }

    void CloseAllMenus()
    {
        foreach (var bmp in buttonsAndMenus)
        {
            bmp.menu.gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class ButtonMenuPair
{
    public Button button;
    public Transform menu;

}
