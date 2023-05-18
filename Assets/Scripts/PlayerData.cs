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
    public float[] BestTimes { get; private set; }

    public List<float>[] Scores { get; private set; }
    public float[] HighScores { get; private set; }

    private int playingLevel;

    #region Scene persistence
    //Declare sole instance of PlayerData
    public static PlayerData Instance;

    //As soon as created
    private void Awake()
    {
        //After first launch, destroy additional instances of PlayerData
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    //On first run
    public void Start()
    {
        //Ensure the number of levels is valid
        if (numLevels < 1)
        {
            numLevels = 1;
        }

        InitialisePlayer();
    }

    //Initialise the player to default values
    public void InitialisePlayer()
    {
        Username = null;

        BestTimes = Enumerable.Repeat(-1f, numLevels).ToArray();
        HighScores = Enumerable.Repeat(0f, numLevels).ToArray();
        LevelsUnlocked = Enumerable.Repeat(false, numLevels).ToArray();
        LevelsUnlocked[0] = true;

        Scores = new List<float>[numLevels];

        Coins = 0;
    }

    //Sets the level the player is playing (-1 for array functionality)
    public void SetPlayingLevel(int level)
    {
        playingLevel = level - 1;
    }

    //Add coins to the player
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

    //Return the best time of the level 
    public float GetBestTime(int level)
    {
        return BestTimes[level];
    }

    //Update a level's best time. Returns true if a new best time is reached
    public bool UpdateBestTimes(float time)
    {
        if (BestTimes[playingLevel] == -1f || BestTimes[playingLevel] < time)
        {
            BestTimes[playingLevel] = time;
            return true;
        }

        return false;
    }

    public void AddScore(int level, int score)
    {
        Scores[level].Add(score);
    }

    //Each levels' scores are separated by a hyphen
    public string ScoresToString()
    {
        string stringScores = "";

        for (int i = 0; i < numLevels; i++)
        {
            string.Join(',', Scores[i]);

            stringScores += ",-";
        }

        return stringScores;
    }

    public float GetHighScore(int level)
    {
        return HighScores[level];
    }

    public bool UpdateHighScores(float score)
    {
        if (HighScores[playingLevel] == -1f || HighScores[playingLevel] < score)
        {
            HighScores[playingLevel] = score;
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
    public void LoadPlayer(string username, string coins, string levelString, string timeString, string highScoreString, string scoresString) 
    {
        Username = username;

        Coins = int.Parse(coins);

        string[] levels = levelString.Split(',');

        for (int i = 0; i < levels.Length; i++)
        {
            LevelsUnlocked[i] = bool.Parse(levels[i]);
        }

        string[] times = timeString.Split(',');

        for (int i = 0; i < times.Length; i++)
        {
            BestTimes[i] = float.Parse(times[i]);
        }

        string[] highScores = highScoreString.Split(',');

        for (int i = 0; i < highScores.Length; i++)
        {
            HighScores[i] = float.Parse(highScores[i]);
        }

        string[] scoresOfEachLevel = timeString.Split('-');

        for (int i = 0; i < scoresOfEachLevel.Length; i++)
        {
            string[] levelScores = timeString.Split(',');

            for (int j = 0; j < levelScores.Length; j++)
            {
                Scores[i].Add(float.Parse(levelScores[j]));
            }
        }
    }
}