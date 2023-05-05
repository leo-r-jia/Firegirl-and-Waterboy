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
        //Set all best times to 0 and only the first level as unlocked
        BestTimes = Enumerable.Repeat(-1f, numLevels).ToArray();
        LevelsUnlocked = Enumerable.Repeat(false, numLevels).ToArray();
        LevelsUnlocked[0] = true;

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
    public void LoadPlayer(string username, string coins, string levelString, string timeString) 
    {
        Username = username;

        Coins = int.Parse(coins);
        
        string[] times = timeString.Split(',');

        for (int i = 0; i < times.Length; i++)
        {
            BestTimes[i] = float.Parse(times[i]);
        }

        string[] levels = levelString.Split(',');

        for (int i = 0; i < levels.Length; i++)
        {
            LevelsUnlocked[i] = bool.Parse(levels[i]);
        }
    }
}