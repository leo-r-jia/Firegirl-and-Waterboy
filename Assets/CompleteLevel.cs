using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] FireExitDoor fireBool;
    [SerializeField] WaterExitDoor waterBool;

    /* Testing */
    [SerializeField] public bool bothDoors = false;
    [SerializeField] bool atWaterDoor = false;
    [SerializeField] public bool atFireDoor = false;


    public void Update()
    {
        CompleteLevelCheck();
    }

    // Changes scene to next level.
    private void CompleteLevelCheck()
    { 
        atWaterDoor = waterBool.getIsAtDoor();
        atFireDoor = fireBool.getIsAtDoor();
    
        if(atFireDoor && atWaterDoor)
        {
            bothDoors = true;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
