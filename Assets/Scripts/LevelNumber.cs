using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNumber : MonoBehaviour
{
    //Update the playerData on what level is being played on scene start
    void Start()
    {
        int levelNumber = int.Parse(SceneManager.GetActiveScene().name.Split(' ')[1]);

        PlayerData.Instance.SetCurrentLevel(levelNumber);
    }
}
