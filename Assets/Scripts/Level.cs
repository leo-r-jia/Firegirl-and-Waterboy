using System.Collections.Generic;
using UnityEngine;

public class Level : ScriptableObject
{
    public int LevelNumber { get; private set; }
    public bool Unlocked {  get; private set; }
    public int HighScore { get; private set; }
    public float BestTime { get; private set; }
    public int MostStars { get; private set; }
    public int MostCoins { get; private set; }
    public List<int> Scores { get; private set; }
    public List<float> Times { get; private set; }
    public List<int> Coins { get; private set; }
    public List<int> Stars { get; private set; }

    public void Initialise(int levelNum)
    {
        LevelNumber = levelNum;

        Scores = new List<int>();
        Times = new List<float>();
        Coins = new List<int>();
        Stars = new List<int>();
    }

    public void SetUnlocked()
    {
        Unlocked = true;
    }

    //Add a new score to the level. Returns -1 if an error occured
    public int AddNewScore(int score, float time, int coins, int stars)
    {
        if (score < 0 || time < 0 || coins < 0 || stars < 0 || stars > 3)
        {
            Debug.Log("Score could not be added as one or more values were out of bounds!\n Score: " + score + " Time: " + time + " Coins: " + coins + " Stars: " + stars);
            return -1;
        }

        Scores.Add(score);
        Times.Add(time);
        Coins.Add(coins);
        Stars.Add(stars);

        return UpdateHighScore(score, time, coins, stars);
    }

    //Update the level's high score and relevant values. Returns 1 if a new high score was reached
    private int UpdateHighScore(int score, float time, int coins, int stars)
    {
        if (score > HighScore)
        {
            HighScore = score;
            BestTime = time;
            MostCoins = coins;
            MostStars = stars;

            return 1;
        }

        return 0;
    }

    //"," separates parts of a score, "-" separates different scores
    public override string ToString()
    {
        string scores = "";

        for (int j = 0; j < Scores.Count; j++)
        {
            scores += Scores[j] + "," + Times[j] + "," + Coins[j] + "," + Stars[j] + "-";
        }

        return scores;
    }
}
