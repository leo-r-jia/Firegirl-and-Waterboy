using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalTutorial : MonoBehaviour
{
    [SerializeField] GameObject gunPickupInstructions;
    [SerializeField] GameObject[] shootControlPanels; 
    [SerializeField] Image[] FiregirlKeys, WaterboyKeys;

    [SerializeField] GameObject exitInstructions;
    [SerializeField] GameObject[] exits;

    void Start()
    {
        
    }

    //Methods invoked from a player (KeyPressed.cs) when pressing a key
    #region Invoked key-pressed methods
    public void WKeyPressed()
    {
        SetHalfTransparency(FiregirlKeys[0]);
        //if (AllKeysPressed())
            //DoCoinTutorial();
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
        //if (coinInstructions.activeSelf || exitInstructions.activeSelf)
        //    return false;

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

    //Hide the gun pickup instruction and show the shoot controls tutorial
    void ShootControlsPanel()
    {
        gunPickupInstructions.SetActive(false);
        foreach (GameObject panel in shootControlPanels)
            panel.SetActive(true);
    }

    //Hide the shoot controls tutorial and show the portal instructions
    public void PortalPanel()
    {
        foreach (GameObject panel in shootControlPanels)
            panel.SetActive(false);
    }

    //Called when the level is complete
    public void HideExitTutorial()
    {
        exitInstructions.SetActive(false);
    }
}
