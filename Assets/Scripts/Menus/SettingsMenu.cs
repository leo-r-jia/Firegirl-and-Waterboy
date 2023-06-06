using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject settingsMenu;

    // Confirms settings and closes settings menu
    public void confirm()
    {
        pauseMenu.SetActive(true); //Need to add functionality
        settingsMenu.SetActive(false);
    }

    // Cancels settings and closes settings menu
    public void cancel()
    {
        pauseMenu.SetActive(true); //Need to add functionality
        settingsMenu.SetActive(false);
    }
}
