using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string Username {  get; set; }
    [SerializeField] private int numLevels;
    public Level[] Levels { get; private set; }
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

        Levels = new Level[numLevels];

        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].Initialise();
        }
    }

    //Sets the level the player is playing (- 1 for array functionality)
    public void SetPlayingLevel(int level)
    {
        playingLevel = level - 1;
    }

    //Unlock the next level
    public void UnlockNextLevel()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            if (!Levels[i].Unlocked)
            {
                Levels[i].SetUnlocked();
                return;
            }
        }
    }

    //Levels separated by "|"
    public string ScoresToString()
    {
        string scores = "";

        //For each level
        for (int i = 0; i < Levels.Length; i++)
        {
            scores += Levels[i].ToString() + "|";
        }

        return scores;
    }

    //Set the player's data to the passed values
    public void LoadPlayer(string username, string unlockedLevelString, string combinedLevelScoresString) 
    {
        Username = username;

        string[] levels = unlockedLevelString.Split(',');
        for (int i = 0; i < levels.Length; i++)
        {
            if (bool.Parse(levels[i]))
            {
                Levels[i].SetUnlocked();
            }
        }

        string[] levelScoresStrings = combinedLevelScoresString.Split("|");
        for (int i = 0; i < levelScoresStrings.Length; i++)
        {
            string[] wholeScoreStrings = levelScoresStrings[i].Split("-");

            for (int j = 0; j < wholeScoreStrings.Length; j++)
            {
                string[] scoreParts = wholeScoreStrings[j].Split(",");

                Levels[i].AddNewScore(int.Parse(scoreParts[0]), float.Parse(scoreParts[1]), int.Parse(scoreParts[2]), int.Parse(scoreParts[3]));
            }
        }
    }
}