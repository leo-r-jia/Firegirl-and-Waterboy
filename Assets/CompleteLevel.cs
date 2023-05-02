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
    private int count = 0;


    public void Update()
    {
        CompleteLevelCheck();
    }

    // Changes scene to next level.
    private void CompleteLevelCheck()
    { 
        atWaterDoor = waterBool.getIsAtDoor();
        atFireDoor = fireBool.getIsAtDoor();
    
        if(atFireDoor && atWaterDoor && count == 0)
        {
            Win();
        }
    }

    private void Win()
    {
        //Time.timeScale = 0f;
        completeLevelMenu.SetActive(true);
        Cursor.visible = true;
        InputSystem.DisableDevice(Keyboard.current);
    }
}
