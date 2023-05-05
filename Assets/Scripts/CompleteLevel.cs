using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] FireExitDoor fireBool;
    [SerializeField] WaterExitDoor waterBool;
    public GameObject completeLevelMenu;

    private bool atWaterDoor = false;
    private bool atFireDoor = false;
    private bool active = false;

    public UnityEvent LevelComplete;

    public void Update()
    {
        CompleteLevelCheck();
    }

    // Checks if both players are at their exits.
    private void CompleteLevelCheck()
    { 
        atWaterDoor = waterBool.getIsAtDoor();
        atFireDoor = fireBool.getIsAtDoor();
    
        if(atFireDoor && atWaterDoor && !active)
        {
            active = true;
            Win();
        }
    }

    // Opens level complete menu
    private void Win()
    {
        Time.timeScale = 0f;
        completeLevelMenu.SetActive(true);
        Cursor.visible = true;
        InputSystem.DisableDevice(Keyboard.current);
        LevelComplete.Invoke();
        PlayFabManager.Instance.SavePlayer();
    }
}
