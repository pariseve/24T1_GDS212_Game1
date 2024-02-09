using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    public GameObject optionsMenu;

    private void Start()
    {
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(false);
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadEndScreen()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenMenu()
    {
        TogglePauseMenu();
    }

    public void CloseMenu()
    {
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(false);
        }
    }

    private void TogglePauseMenu()
    {
        if(optionsMenu != null)
        {
            optionsMenu.SetActive(true);
        }
    }
}
