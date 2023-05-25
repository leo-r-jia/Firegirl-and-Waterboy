using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text levelTitle;
    [SerializeField] private TMP_Text noHighScore;
    [SerializeField] private TMP_Text scoreValue;
    [SerializeField] private Button playButton;
    [SerializeField] private ChangeScene changeScene;

    [SerializeField] private Button personalButton;
    [SerializeField] private Button globalButton;

    [SerializeField] private GameObject personalLeaderboard;
    [SerializeField] private GameObject personalRowPrefab;
    [SerializeField] private Transform personalRowsParent;
    [SerializeField] private GameObject globalLeaderboard;
    [SerializeField] private GameObject globalRowPrefab;
    [SerializeField] private Transform globalRowsParent;

    private void OnEnable()
    {
        Initialise();

        SetHighScore();

        SetPersonalLeaderboard();

        if (PlayFabManager.Instance.LoggedInAsGuest || PlayerData.Instance.Username != null)
        {
            GetGlobalLeaderboard();
        }
    }

    //Restore to default state
    private void Initialise()
    {
        if (scoreValue.gameObject.activeSelf) ToggleHighScore();

        if (!personalLeaderboard.activeSelf) SwitchLeaderboards();

        levelTitle.text = "LEVEL " + (PlayerData.Instance.CurrentLevel + 1);

        PlayFabManager.Instance.LeaderboardGet.RemoveAllListeners();
        PlayFabManager.Instance.LeaderboardGet.AddListener(SetGlobalLeaderboard);
        PlayFabManager.Instance.GuestLoggedIn.RemoveAllListeners();
        PlayFabManager.Instance.GuestLoggedIn.AddListener(GetGlobalLeaderboard);
    }

    private void ToggleHighScore()
    {
        noHighScore.gameObject.SetActive(!noHighScore.gameObject.activeSelf);
        scoreValue.gameObject.SetActive(!scoreValue.gameObject.activeSelf);
    }

    private void SetHighScore()
    {
        if (PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore.ScoreValue == 0) return;

        ToggleHighScore();

        scoreValue.text = "" + PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore.ScoreValue;
    }

    private void SetPersonalLeaderboard()
    {
        ClearLeaderboard(personalRowsParent);

        List<Score> sortedScores = PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].SortScores();

        foreach (Score score in sortedScores)
        {
            GameObject newGo = Instantiate(personalRowPrefab, personalRowsParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = score.ScoreValue.ToString();
            texts[1].text = score.Time.ToString();
            texts[2].text = score.Coins.ToString() + "/6";
            texts[3].text = score.Stars.ToString() + "/3";
        }
    }

    //Request the global leaderboard from the PlayFabManager
    public void GetGlobalLeaderboard()
    {
        PlayFabManager.Instance.GetLeaderboard(PlayerData.Instance.CurrentLevel + 1);
    }

    //Invoked when PlayFabManager retrieves a leaderboard successfully
    private void SetGlobalLeaderboard()
    {
        ClearLeaderboard(globalRowsParent);

        int rank = 1;

        foreach (GlobalScore score in PlayFabManager.Instance.GlobalLevelLeaderboard)
        {
            GameObject newGo = Instantiate(globalRowPrefab, globalRowsParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = rank++.ToString();
            texts[1].text = score.PlayerName;
            texts[2].text = score.ScoreValue.ToString();
        }
    }

    private void ClearLeaderboard(Transform rowParent)
    {
        foreach (Transform child in rowParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SwitchLeaderboards()
    {
        personalButton.interactable = !personalButton.interactable;
        globalButton.interactable = !globalButton.interactable;

        personalLeaderboard.SetActive(!personalLeaderboard.activeSelf);
        globalLeaderboard.SetActive(!globalLeaderboard.activeSelf);
    }

    //Needed as initial reference to PlayerData is lost on scene change
    public void SetPlayerDataCurrentLevel(int level)
    {
        PlayerData.Instance.SetCurrentLevel(level);
    }

    public void PlayLevel()
    {
        changeScene.LoadScene("Level " + (PlayerData.Instance.CurrentLevel + 1));
    }
}