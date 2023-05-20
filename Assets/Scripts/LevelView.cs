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

        SetHighScore();
    }

    private void Initialise()
    {
        noHighScore.gameObject.SetActive(true);
        highScore.gameObject.SetActive(false);
        coins.gameObject.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void SetHighScore()
    {
        if (PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore == 0)
        {
            return;
        }

        playButton.onClick.AddListener(GoToLevel);

        noHighScore.gameObject.SetActive(false);
        highScore.gameObject.SetActive(true);
        coins.gameObject.SetActive(true);

        highScore.text = "" + PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].HighScore;
        coins.text = PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].MostCoins + "/6";

        for (int i = 0; i < PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].MostStars; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void GoToLevel()
    {
        changeScene.LoadScene("Level " + (PlayerData.Instance.CurrentLevel + 1));
    }

    public void SwitchLeaderBoards()
    {

    }
}
