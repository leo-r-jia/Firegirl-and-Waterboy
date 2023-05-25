using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class LeaderboardTests
{
    private Transform LevelView;

    //Set up the scene for testing leaderboards
    [UnitySetUp]
    public IEnumerator SetUp()
    {   
        SceneManager.LoadScene("Scenes/Dylan/Main Menu");

        yield return null;

        GameObject canvas = GameObject.Find("Canvas");
        Transform LevelMenu = canvas.transform.Find("LevelMenu");

        canvas.transform.GetChild(1).gameObject.SetActive(false);
        LevelMenu.gameObject.SetActive(true);

        LevelView = LevelMenu.GetChild(6);

        Button btn = LevelMenu.GetChild(2).GetComponent<Button>();
        btn.onClick.Invoke();

        PlayFabManager.Instance.LoginAsGuest();

        yield return new WaitForSecondsRealtime(3);
    }

    [Test]
    public void SetPlayerDataCurrentLevel_WhenExecuted_SetsPlayerCurrentLevel()
    {
        int level = 1;

        LevelView.gameObject.GetComponent<LevelView>().SetPlayerDataCurrentLevel(level);

        int actual = PlayerData.Instance.CurrentLevel + 1;

        Assert.AreEqual(level, actual);
    }

    [Test]
    public void SetPlayerDataCurrentLevel_ToNegativeNumber_ThrowsException()
    {
        int level = -1;

        try
        {
            LevelView.gameObject.GetComponent<LevelView>().SetPlayerDataCurrentLevel(level);
            Assert.Fail("Expected exception");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void SetPlayerDataCurrentLevel_ToNotExistingLevel_ThrowsException()
    {
        int level = 100;

        try
        {
            LevelView.gameObject.GetComponent<LevelView>().SetPlayerDataCurrentLevel(level);
            Assert.Fail("Expected exception");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }

    [Test] 
    public void SwitchLeaderBoards_WhenExecuted_ChangesActiveLeaderboards()
    {
        bool personalBefore = LevelView.GetChild(8).gameObject.activeSelf;
        bool globalBefore = LevelView.GetChild(9).gameObject.activeSelf;

        LevelView.gameObject.GetComponent<LevelView>().SwitchLeaderboards();

        bool personalActual = LevelView.GetChild(8).gameObject.activeSelf;
        bool globalActual = LevelView.GetChild(9).gameObject.activeSelf;

        Assert.True((personalBefore != personalActual) && (globalBefore != globalActual));
    }

    [UnityTest]
    public IEnumerator GetGlobalLeaderBoard_WhenExecuted_GetsLevelGlobalLeaderboard()
    {
        GameObject globalRowsParent = LevelView.GetChild(9).GetChild(4).GetChild(0).gameObject;
        foreach (Transform child in globalRowsParent.transform)
        {
            UnityEngine.Object.Destroy(child.gameObject);
        }

        int before = globalRowsParent.transform.childCount;

        LevelView.gameObject.GetComponent<LevelView>().GetGlobalLeaderboard();

        yield return null;

        int actual = globalRowsParent.transform.childCount;

        Assert.AreNotEqual(before, actual);
    }

    [UnityTest]
    public IEnumerator PlayLevel_WhenExecuted_StartsTheLevel()
    {
        string expected = "Level 1";

        LevelView.gameObject.GetComponent<LevelView>().PlayLevel();

        yield return null;

        string actual = SceneManager.GetActiveScene().name;

        Assert.AreEqual(expected, actual);
    }
}