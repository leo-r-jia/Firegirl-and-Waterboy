using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string Username {  get; set; }

    [SerializeField] private int numLevels;
    public int Coins { get; private set; }
    public bool[] LevelsUnlocked { get; private set; }
    public int[] HighScores { get; private set; }

    public void Start()
    {
        if (Username == null)
        {
            //Ensure the number of levels is valid
            if (numLevels < 1)
            {
                numLevels = 1;
            }

            InitialisePlayer();
        }
    }

    //Initialise the player to default values
    public void InitialisePlayer()
    {
        Username = string.Empty;

        //Set all high scores to 0 and only the first level as unlocked
        HighScores = Enumerable.Repeat(0, numLevels).ToArray();
        LevelsUnlocked = Enumerable.Repeat(false, numLevels).ToArray();
        LevelsUnlocked[0] = true;

        Coins = 0;
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    //Remove Coins if possible. Returns true if Coins could be removed
    public bool RemoveCoins(int amount)
    {
        if (amount > Coins)
        {
            return false;
        }

        Coins -= amount;
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

    //Unlock the next level -> set the next false to true
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

    //Set the player's data to the passed values
    public void LoadPlayer(string username, string coins, string levelString, string scoreString) 
    {
        Username = username;

        Coins = int.Parse(coins);
        
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