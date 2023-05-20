using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text levelTitle;
    [SerializeField] private TMP_Text noHighScore;
    [SerializeField] private TMP_Text highScore;
    [SerializeField] private TMP_Text coins;
    [SerializeField] private GameObject stars;
    [SerializeField] private Button playButton;
    [SerializeField] private ChangeScene changeScene;


    private void OnEnable()
    {
        Initialise();

        levelTitle.text = "LEVEL " + (PlayerData.Instance.CurrentLevel + 1);

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(GoToLevel);

        SetHighScore();


    }

    //Restore to default state
    private void Initialise()
    {
        if (highScore.gameObject.activeSelf)
        {
            ToggleHighScore();
        }

        for (int i = 0; i < 3; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void ToggleHighScore()
    {
        noHighScore.gameObject.SetActive(!noHighScore.gameObject.activeSelf);
        highScore.gameObject.SetActive(!highScore.gameObject.activeSelf);
        coins.gameObject.SetActive(!coins.gameObject.activeSelf);
    }

    private void SetHighScore()
    {
        if (PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore.ScoreValue == 0)
        {
            return;
        }

        ToggleHighScore();

        highScore.text = "" + PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore.ScoreValue;
        coins.text = PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore.Coins + "/6";

        for (int i = 0; i < PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore.Stars; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    //Needed as initial reference to PlayerData is lost on scene change
    public void SetPlayerDataCurrentLevel(int level)
    {
        PlayerData.Instance.SetCurrentLevel(level);
    }

    private void GoToLevel()
    {
        changeScene.LoadScene("Level " + (PlayerData.Instance.CurrentLevel + 1));
    }

    public void SwitchLeaderBoards()
    {

    }
}
