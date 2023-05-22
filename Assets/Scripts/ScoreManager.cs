using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    //Live coin count
    private int coins = 0;
    [SerializeField] public TextMeshProUGUI coinsCollected;

    //Fields displayed upon level completion
    [SerializeField] public TextMeshProUGUI coinsCollectedLevelComplete;
    [SerializeField] public TextMeshProUGUI scoreLevelComplete;

    [SerializeField] public TextMeshProUGUI stars;

    [SerializeField] public Timer timer;
    private int score = 0;

    //Update coins collected
    public void UpdateScore()
    {
        coins += 1;
        coinsCollected.text = "Coins: " + coins;
    }
}
