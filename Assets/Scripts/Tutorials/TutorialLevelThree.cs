using UnityEngine;

public class TutorialLevelThree : MonoBehaviour
{
    [SerializeField] LayerMask[] playerLayers;

    [SerializeField] Transform boxPoint1, boxPoint2, switchPoint1, switchPoint2;
    [SerializeField] GameObject boxInstructions, firstSwitchInstructions, secondSwitchInstructions;

    bool boxTutorialDone = false; 
    bool firstSwitchTutorialDone = false;

    private void Update()
    {
        if (!boxTutorialDone && (PlayerIsNearPoint(boxPoint1) || PlayerIsNearPoint(boxPoint2)))
        {
            boxTutorialDone = true;
            boxInstructions.SetActive(true);
        }

        if (!firstSwitchTutorialDone && PlayerIsNearPoint(switchPoint1))
        {
            firstSwitchTutorialDone = true;
            boxInstructions.SetActive(false);
            firstSwitchInstructions.SetActive(true);
        }

        if (PlayerIsNearPoint(switchPoint2))
        {
            firstSwitchInstructions.SetActive(false);
            secondSwitchInstructions.SetActive(true);
        }
    }

    //If a player is near a specified point
    bool PlayerIsNearPoint(Transform point)
    {
        if (Physics2D.OverlapCircle(point.position, 2f, playerLayers[0]) || Physics2D.OverlapCircle(point.position, 3f, playerLayers[1]))
            return true;

        return false;
    }
}
