using UnityEngine;

public class TutorialLevelTwo : MonoBehaviour
{
    [SerializeField] LayerMask[] playerLayers;

    [SerializeField] Transform waterPoint, lavaPoint, acidPoint;
    [SerializeField] GameObject waterInstructions, lavaInstructions, acidInstructions;

    bool waterTutorialDone = false; 
    bool lavaTutorialDone = false;

    private void Update()
    {
        if (!waterTutorialDone && PlayerIsNearPoint(waterPoint))
        {
            waterTutorialDone = true;
            waterInstructions.SetActive(true);
        }

        if (!lavaTutorialDone && PlayerIsNearPoint(lavaPoint))
        {
            lavaTutorialDone = true;
            waterInstructions.SetActive(false);
            lavaInstructions.SetActive(true);
        }

        if (PlayerIsNearPoint(acidPoint))
        {
            lavaInstructions.SetActive(false);
            acidInstructions.SetActive(true);
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
