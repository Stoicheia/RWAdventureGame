using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookNavigationUI : MonoBehaviour
{
    [SerializeField] private List<ButtonMenuPair> buttonsAndMenus;
    [SerializeField] private Transform startingMenu;
    [SerializeField] private Button startingButton;
    
    private Transform activeMenu;
    private Button activeButton;

    private void OnEnable()
    {
        foreach (var bmp in buttonsAndMenus)
        {
            bmp.button.onClick.AddListener(delegate { OpenMenu(bmp.menu, bmp.button); });
        }
    }

    private void OnDisable()
    {
        foreach (var bmp in buttonsAndMenus)
        {
            bmp.button.onClick.RemoveAllListeners();
        }
    }

    private void Start()
    {
        OpenStartingMenu();
    }

    void OpenMenu(Transform menuToOpen, Button clickedButton)
    {
        if(activeMenu!=null) activeMenu.gameObject.SetActive(false);
        menuToOpen.gameObject.SetActive(true);

        if (activeButton != null)
        {
            ActivableTabNavUI u = activeButton.GetComponent<ActivableTabNavUI>();
            u.Deactivate();
        }
        clickedButton.GetComponent<ActivableTabNavUI>().Activate();
        
        activeMenu = menuToOpen;
        activeButton = clickedButton;
    }

    public void OpenStartingMenu()
    {
        OpenMenu(startingMenu, startingButton);
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
