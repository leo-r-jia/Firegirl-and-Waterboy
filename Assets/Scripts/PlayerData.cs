using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int numLevels;
    public int Gems { get; private set; }
    public bool[] LevelsUnlocked { get; private set; }
    public int[] HighScores { get; private set; }

    public void Start()
    {
        //Ensure the number of levels is valid
        if (numLevels < 1)
        {
            numLevels = 1;
        }

        //Set all high scores to 0 and only the first level as unlocked
        HighScores = Enumerable.Repeat(0, numLevels).ToArray();
        LevelsUnlocked = Enumerable.Repeat(false, numLevels).ToArray();
        LevelsUnlocked[0] = true;

        Gems = 0;
    }

    public void AddGems(int amount)
    {
        Gems += amount;
    }

    //Remove gems if possible. Returns true if gems could be removed
    public bool RemoveGems(int amount)
    {
        if (amount > Gems)
        {
            return false;
        }

        Gems -= amount;
        return true;
    }

    //Return the high score of the level 
    public int GetHighScore(int level)
    {
        return HighScores[level];
    }

    //Update a level's high score. Returns true if a new high score is reached
    public bool UpdateHighScore(int level, int score)
    {
        if (HighScores[level] < score)
        {
            HighScores[level] = score;
            return true;
        }

        return false;
    }

    public bool LevelUnlocked(int level)
    {
        return LevelsUnlocked[level];
    }

    public void UnlockNextLevel()
    {
        for (int i = 0; i < LevelsUnlocked.Length; i++)
        {
            if (!LevelsUnlocked[i])
            {
                LevelsUnlocked[i] = true;
                return;
            }
        }
    }

    public void LoadPlayer(string gems, string levelString, string scoreString) 
    {
        Gems = int.Parse(gems);
        
        string[] scores = scoreString.Split(',');

        for (int i = 0; i < scores.Length; i++)
        {
            HighScores[i] = int.Parse(scores[i]);
        }

        string[] levels = levelString.Split(',');

        for (int i = 0; i < levels.Length; i++)
        {
            LevelsUnlocked[i] = bool.Parse(levels[i]);
        }
    }
}