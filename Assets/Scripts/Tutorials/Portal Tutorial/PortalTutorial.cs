using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalTutorial : MonoBehaviour
{
    [SerializeField] GameObject gunPickupInstructions;
    [SerializeField] GameObject[] shootControlPanels; 
    [SerializeField] Image FiregirlKey, WaterboyKey;

    [SerializeField] GameObject blueGun, redGun;

    [SerializeField] GameObject blueGunInHand, redGunInHand;

    [SerializeField] GameObject portalInstructions;

    bool blueShot = false;
    bool redShot = false;

    void Update()
    {
        if (blueGun == null && redGun == null)
        {
            ShootControlsPanel();
        }
        if (blueGunInHand.GetComponent<PortalGun>().shootKeyPressed)
        {
            SetHalfTransparency(WaterboyKey);
            blueShot = true;
        }
        if (redGunInHand.GetComponent<PortalGun>().shootKeyPressed)
        {
            SetHalfTransparency(FiregirlKey);
            redShot = true;
        }
        if (redShot && blueShot)
        {
            PortalPanel();
        }
    }

    //Set an image of a key to half transparency
    void SetHalfTransparency(Image keyImage)
    {
        Color tempColour = keyImage.color;
        tempColour.a = .5f;
        keyImage.color = tempColour;
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
        portalInstructions.SetActive(true);
    }
}
