using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] public TextMeshProUGUI coinsCollected;

    [SerializeField] public Timer timer;
    public int score;

    //Update coins collected
    public void UpdateScore()
    {
        coins += 1;
        coinsCollected.text = "Coins: " + coins;
    }

    //Calculate score on level complete
    public void calculateFinalScore()
    {
        score = 0;
        //Calculate final score based on coins collected and time taken
        score += coins * 10000 + 5000 - (int)Math.Ceiling(timer.currentTime);
    }

    //Add the coins collected this level to the player
    public void AddCoinsToPlayer()
    {
        PlayerData.Instance.AddCoins(coins);
    }
}
