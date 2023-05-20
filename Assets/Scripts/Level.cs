using System.Collections.Generic;
using UnityEngine;

public class Level : ScriptableObject
{
    public int LevelNumber { get; private set; }
    public bool Unlocked {  get; private set; }
    public Score HighScore { get; private set; }
    public List<Score> Scores { get; private set; }

    public void Initialise(int levelNum)
    {
        LevelNumber = levelNum;

        HighScore = ScriptableObject.CreateInstance<Score>();
        Scores = new List<Score>();
    }

    public void SetUnlocked()
    {
        Unlocked = true;
    }

    //Add a new score to the level. Returns -1 if an error occured
    public int AddNewScore(int score, float time, int coins, int stars)
    {
        if (score < 0 || time < 0 || coins < 0 || coins > 6 || stars < 0 || stars > 3)
        {
            Debug.Log("Score could not be added as one or more values were out of bounds!\n Score: " + score + " Time: " + time + " Coins: " + coins + " Stars: " + stars);
            return -1;
        }

        Scores.Add(ScriptableObject.CreateInstance<Score>());
        Scores[^1].SetScore(score, time, coins, stars);


        return UpdateHighScore(score, time, coins, stars);
    }

    //Update the level's high score and relevant values. Returns 1 if a new high score was reached
    private int UpdateHighScore(int score, float time, int coins, int stars)
    {
        if (score > HighScore.ScoreValue)
        {
            HighScore.SetScore(score, time, coins, stars);

            return 1;
        }

        return 0;
    }

    //"," separates parts of a score, ":" separates different scores
    public override string ToString()
    {
        string scores = "";

        for (int j = 0; j < Scores.Count; j++)
        {
            scores += Scores[j].ScoreValue + "," + Scores[j].Time + "," + Scores[j].Coins + "," + Scores[j].Stars;

            if (j < Scores.Count - 1)
            {
                scores += ":";
            }
        }

        return scores;
    }
}
