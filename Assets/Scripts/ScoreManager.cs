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

    [SerializeField] public Timer timer;
    private int score = 0;

    //Update coins collected
    public void UpdateScore()
    {
        coins += 1;
        coinsCollected.text = "Coins: " + coins;
    }

    //Level complete
    public void LevelComplete()
    {
        CalculateFinalScore();
        coinsCollectedLevelComplete.text = coins + "/6";
        scoreLevelComplete.text = score +"";
    }

    //Calculate score on level complete
    private void CalculateFinalScore()
    {
        score = 0;
        //Calculate final score based on coins collected and time taken
        score = coins * 10000 + (int)((5000 - timer.GetTime()) * (1 + 0.01 * coins));
    }

    //Add the coins collected this level to the player
    public void AddCoinsToPlayer()
    {
        PlayerData.Instance.AddCoins(coins);
    }
}
