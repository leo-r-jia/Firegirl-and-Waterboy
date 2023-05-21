using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject settingsMenu;

    // Music slider
    public void SetMusic (float musicVolume)
    {
        Debug.Log(musicVolume);
    }

    // SFX slider
    public void SetSFX (float sfxVolume)
    {
        Debug.Log(sfxVolume);
    }

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
