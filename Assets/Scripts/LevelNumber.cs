using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNumber : MonoBehaviour
{
    //Update the playerData on what level is being played on scene start
    void Start()
    {
        int levelNumber = int.Parse(transform.name.Split(' ')[1]);

        PlayerData.Instance.SetCurrentLevel(levelNumber);
    }
}
