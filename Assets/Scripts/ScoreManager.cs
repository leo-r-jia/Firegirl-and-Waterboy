using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] public TextMeshProUGUI coinsCollected;

    public void UpdateScore()
    {
        coins += 1;
        coinsCollected.text = "Coins: " + coins;
    }

    //Add the coins collected this level to the player
    public void AddCoinsToPlayer()
    {
        PlayerData.Instance.AddCoins(coins);
    }
}
