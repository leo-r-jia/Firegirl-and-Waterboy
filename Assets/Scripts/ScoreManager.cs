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
    private int starsValue;

    [SerializeField] public Timer timer;
    private int score = 0;

    //Level complete
    public void LevelComplete()
    {
        CalculateFinalScore();
        CalculateStars();
        coinsCollectedLevelComplete.text = coins + "/6";
        scoreLevelComplete.text = score + "";

        PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].AddNewScore(score, timer.GetTime(), coins, starsValue);
        PlayFabManager.Instance.SavePlayer();
    }

    //Update coins collected
    public void UpdateScore()
    {
        coins += 1;
        coinsCollected.text = "Coins: " + coins;
    }

    private void CalculateStars()
    {
        if (score < 10000)
        {
            stars.text = "☆☆☆";
            starsValue = 0;
        }
        else if (score <= 22500)
        {
            stars.text = "★☆☆";
            starsValue = 1;
        }
        else if (score > 22000 && score <= 60000)
        {
            stars.text = "★★☆";
            starsValue = 2;
        }
        else
        {
            stars.text = "★★★";
            starsValue = 3;
        }
    }

    //Calculate score on level complete
    private void CalculateFinalScore()
    {
        score = 0;
        //Calculate final score based on coins collected and time taken
        score = coins * 10000 + (5000 - timer.GetTime());
    }
}
