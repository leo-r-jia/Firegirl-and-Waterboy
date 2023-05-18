using System.Collections; 
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string Username {  get; set; }
    [SerializeField] Transform levelMenu;
    private int numLevels;
    public Level[] Levels { get; private set; }
    public int currentLevel { get; private set; }

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
        numLevels = 0;

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
            Levels[i].Initialise();
        }
    }

    //Sets the level the player is playing (- 1 for array functionality)
    public void SetCurrentLevel(int level)
    {
        currentLevel = level - 1;
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

        return unlockedLevels;
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