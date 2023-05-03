using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] FireExitDoor fireBool;
    [SerializeField] WaterExitDoor waterBool;
    public GameObject completeLevelMenu;

    private bool atWaterDoor = false;
    private bool atFireDoor = false;
    private bool active = false;


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
            Win();
            active = true;
        }
    }

    // Opens level complete menu
    private void Win()
    {
        Time.timeScale = 0f;
        Debug.Log("Freeze");
        completeLevelMenu.SetActive(true);
        Cursor.visible = true;
        InputSystem.DisableDevice(Keyboard.current);
    }
}
