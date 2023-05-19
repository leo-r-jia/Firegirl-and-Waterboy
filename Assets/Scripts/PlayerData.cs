using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public string Username {  get; set; }
    [SerializeField] Transform levelMenu;
    private int numLevels;
    public Level[] Levels { get; private set; }
    //CurrentLevel is in terms of the Levels array
    public int CurrentLevel { get; private set; }

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

    //On first run set the number of levels and initialise
    public void Start()
    {
        foreach (Transform child in levelMenu.transform.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.transform.name.ContainsInsensitive("level"))
            {
                numLevels++;
            }
        }

        numLevels -= 1;

        InitialisePlayer();
    }

    //Initialise the player to default values
    public void InitialisePlayer()
    {
        Username = null;

        Levels = new Level[numLevels];

        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i] = ScriptableObject.CreateInstance<Level>();
            Levels[i].Initialise(i + 1);
        }

        UnlockNextLevel();
    }

    //Sets the level the player is playing (- 1 for array functionality)
    public void SetCurrentLevel(int level)
    {
        CurrentLevel = level - 1;
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

    public string UnlockedLevelsToString()
    {
        string unlockedLevels = "";

        for (int i = 0; i < Levels.Length; i++)
        {
            unlockedLevels += Levels[i].Unlocked + ",";
        }

        unlockedLevels = unlockedLevels[0..^1];

        return unlockedLevels;
    }

    //Levels separated by "|"
    public string ScoresToString()
    {
        string scores = "";

        //For each level
        for (int i = 0; i < Levels.Length; i++)
        {
            scores += Levels[i].ToString();

            if (i < Levels.Length - 1)
            {
                scores += "|";
            }
        }

        return scores;
    }

    //Set the player's data from the passed values
    public void LoadPlayer(string username, string unlockedLevelString, string combinedLevelScoresString) 
    {
        Username = username;

        RestoreUnlockedLevels(unlockedLevelString);
        RestoreScores(combinedLevelScoresString);
    }

    //Restore what levels the player had unlocked
    private void RestoreUnlockedLevels(string unlockedLevelString)
    {
        string[] unlockedLevels = unlockedLevelString.Split(',');
        for (int i = 0; i < unlockedLevels.Length; i++)
        {
            if (bool.Parse(unlockedLevels[i]))
            {
                Levels[i].SetUnlocked();
            }
        }

        //If a new level has been added and the player had completed all other levels beforehand
        if (unlockedLevels.Length < Levels.Length && bool.Parse(unlockedLevels[^1]))
        {
            UnlockNextLevel();
        }
    }

    //Restore the player's scores
    private void RestoreScores(string combinedLevelScoresString)
    {
        string[] levelScoresStrings = combinedLevelScoresString.Split("|");
        for (int i = 0; i < levelScoresStrings.Length; i++)
        {
            string[] wholeScoreStrings = levelScoresStrings[i].Split(":");

            for (int j = 0; j < wholeScoreStrings.Length; j++)
            {
                if (wholeScoreStrings[j].Length == 0)
                {
                    continue;
                }

                string[] scoreParts = wholeScoreStrings[j].Split(",");

                Levels[i].AddNewScore(int.Parse(scoreParts[0]), float.Parse(scoreParts[1]), int.Parse(scoreParts[2]), int.Parse(scoreParts[3]));
            }
        }
    }
}