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

    //Level complete
    public void LevelComplete()
    {
        CalculateFinalScore();
        CalculateStars();
        coinsCollectedLevelComplete.text = coins + "/6";
        scoreLevelComplete.text = score +"";
    }

    private void CalculateStars()
    {
        if(score < 10000)
        {
            stars.text = "☆☆☆";
        }
        else if (score <= 22500)
        {
            stars.text = "★☆☆";
        } else if (score > 22000 && score <= 60000)
        {
            stars.text = "★★☆";
        } else
        {
            stars.text = "★★★";
        }
    }

    //Calculate score on level complete
    private void CalculateFinalScore()
    {
        score = 0;
        //Calculate final score based on coins collected and time taken
        score = coins * 10000 + (5000 - timer.GetTime());
    }

    //Add the coins collected this level to the player
    public void AddCoinsToPlayer()
    {
        PlayerData.Instance.AddCoins(coins);
    }
}
