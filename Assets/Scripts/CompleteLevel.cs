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
    [SerializeField] PlayerMovement player1;
    [SerializeField] PlayerMovement player2;

    //Score manager called upon level completion
    [SerializeField] ScoreManager scoreManager;

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
        atWaterDoor = waterBool.IsAtDoor();
        atFireDoor = fireBool.IsAtDoor();
    
        if(atFireDoor && atWaterDoor && !active && BothPlayersAreGrounded())
        {
            active = true;
            Win();
        }
    }

    private bool BothPlayersAreGrounded()
    {
        if (!player1.IsGrounded() || !player2.IsGrounded())
            return false;

        return true;
    }

    // Opens level complete menu
    private void Win()
    {
        AudioManager.Instance.PlayMusic("Menu Theme");
        Time.timeScale = 0f;
        completeLevelMenu.SetActive(true);
        scoreManager.LevelComplete();
        Cursor.visible = true;
        InputSystem.DisableDevice(Keyboard.current);
        LevelComplete.Invoke();
        PlayFabManager.Instance.SavePlayer();
    }
}
