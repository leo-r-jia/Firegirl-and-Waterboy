using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private int levelNumber = 0;

    //Update the playerData on what level is being played on scene start
    void Start()
    {
        PlayerData.Instance.SetPlayingLevel(levelNumber);
    }
}
