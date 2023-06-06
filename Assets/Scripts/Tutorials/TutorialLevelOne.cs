using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialLevelOne : MonoBehaviour
{
    [SerializeField] GameObject controlInstructions;
    [SerializeField] GameObject[] controlPanels; 
    [SerializeField] Image[] FiregirlKeys, WaterboyKeys;

    [SerializeField] GameObject coinInstructions;
    Transform collectibles;

    [SerializeField] GameObject exitInstructions;
    [SerializeField] GameObject[] exits;

    void Start()
    {
        collectibles = GameObject.Find("Collectibles").transform;
    }

    //Methods invoked from a player (KeyPressed.cs) when pressing a key
    #region Invoked key-pressed methods
    public void WKeyPressed()
    {
        SetHalfTransparency(FiregirlKeys[0]);
        if (AllKeysPressed())
            DoCoinTutorial();
    }

    public void AKeyPressed()
    {
        SetHalfTransparency(FiregirlKeys[1]);
        if (AllKeysPressed())
            DoCoinTutorial();
    }

    public void DKeyPressed()
    {
        SetHalfTransparency(FiregirlKeys[2]);
        if (AllKeysPressed())
            DoCoinTutorial();
    }

    public void UpArrowKeyPressed()
    {
        SetHalfTransparency(WaterboyKeys[0]);
        if (AllKeysPressed())
            DoCoinTutorial();
    }

    public void LeftArrowKeyPressed()
    {
        SetHalfTransparency(WaterboyKeys[1]);
        if (AllKeysPressed())
            DoCoinTutorial();
    }

    public void RightArrowKeyPressed()
    {
        SetHalfTransparency(WaterboyKeys[2]);
        if (AllKeysPressed())
            DoCoinTutorial();
    }
    #endregion

    //Set an image of a key to half transparency
    void SetHalfTransparency(Image keyImage)
    {
        Color tempColour = keyImage.color;
        tempColour.a = .5f;
        keyImage.color = tempColour;
    }

    //Check if all keys have been pressed
    bool AllKeysPressed()
    {
        if (coinInstructions.activeSelf || exitInstructions.activeSelf)
            return false;

        foreach (Image image in FiregirlKeys)
        {
            if (image.color.a != .5f)
                return false;
        }

        foreach (Image image in WaterboyKeys)
        {
            if (image.color.a != .5f)
                return false;
        }

        return true;
    }

    //Hide the controls tutorial and show the coin tutorial
    void DoCoinTutorial()
    {
        controlInstructions.SetActive(false);
        foreach (GameObject panel in controlPanels)
            panel.SetActive(false);

        coinInstructions.SetActive(true);
        for (int i = 0; i < collectibles.childCount; i++)
            collectibles.GetChild(i).gameObject.SetActive(true);
    }

    //Hide the coin tutorial and show the exit tutorial. Invoked from ItemCollector.cs
    public void DoExitTutorial()
    {
        if (AllCoinsColllected())
        {
            coinInstructions.SetActive(false);

            exitInstructions.SetActive(true);

            foreach (GameObject exit in exits)
                exit.SetActive(true);
        }
    }

    //If all coins in the scene have been collected
    bool AllCoinsColllected()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        if (coins.Length == 1)
            return true;

        return false;
    }

    //Called when the level is complete
    public void HideExitTutorial()
    {
        exitInstructions.SetActive(false);
    }
}
