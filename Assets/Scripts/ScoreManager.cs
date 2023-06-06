using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    //Live coin count
    private int coins = 0;

    private int customTime = 0; // Added for testing purposes

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
        PlayerData.Instance.UnlockNextLevel();
        PlayFabManager.Instance.SavePlayer();
    }

    //Update coins collected
    public void UpdateScore()
    {
        coins += 1;
        coinsCollected.text = "Coins: " + coins;
    }

    //Calculate the number of stars to display based on score
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
    public void CalculateFinalScore()
    {
        score = 0;
        // Calculate final score based on coins collected and time taken
        score = coins * 10000 + (5000 - GetTime()); 
    }

    //Get and set methods for coins
    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    //Get and set methods for score
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    // New method to get time (used for testing)
    private int GetTime()
    {
        return (timer != null) ? timer.GetTime() : customTime;
    }

    // Testing method to set custom time
    public void SetCustomTime(int time)
    {
        customTime = time;
    }
}
