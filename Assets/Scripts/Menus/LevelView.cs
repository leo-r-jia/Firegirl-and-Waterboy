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
    [SerializeField] private GameObject stars;
    [SerializeField] private Button playButton;
    [SerializeField] private ChangeScene changeScene;

    [SerializeField] private Button personalButton;
    [SerializeField] private Button globalButton;

    [SerializeField] private GameObject personalRowPrefab;
    [SerializeField] private Transform personalRowsParent;


    private void OnEnable()
    {
        Initialise();

        levelTitle.text = "LEVEL " + (PlayerData.Instance.CurrentLevel + 1);

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(GoToLevel);

        SetHighScore();

        SetPersonalLeaderboard();
    }

    //Restore to default state
    private void Initialise()
    {
        if (scoreValue.gameObject.activeSelf)
        {
            ToggleHighScore();
        }

        for (int i = 0; i < 3; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(false);
        }

        foreach (Transform child in personalRowsParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ToggleHighScore()
    {
        noHighScore.gameObject.SetActive(!noHighScore.gameObject.activeSelf);
        scoreValue.gameObject.SetActive(!scoreValue.gameObject.activeSelf);
    }

    private void SetHighScore()
    {
        if (PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore.ScoreValue == 0)
        {
            return;
        }

        ToggleHighScore();

        scoreValue.text = "" + PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore.ScoreValue;

        for (int i = 0; i < PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore.Stars; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void SetPersonalLeaderboard()
    {
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

    //Needed as initial reference to PlayerData is lost on scene change
    public void SetPlayerDataCurrentLevel(int level)
    {
        PlayerData.Instance.SetCurrentLevel(level);
    }

    private void GoToLevel()
    {
        gameObject.SetActive(false);
        changeScene.LoadScene("Level " + (PlayerData.Instance.CurrentLevel + 1));
    }

    public void SwitchLeaderBoards()
    {
        personalButton.interactable = !personalButton.interactable;
        globalButton.interactable = !globalButton.interactable;
    }
}
