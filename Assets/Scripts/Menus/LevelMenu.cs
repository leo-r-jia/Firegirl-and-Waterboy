using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private bool doLevelLocking = false;

    private void OnEnable()
    {
        if (doLevelLocking) UnlockLevels();
    }

    private void UnlockLevels()
    {
        //For every level button, set its state
        foreach (Transform level in transform.GetComponentsInChildren<Transform>())
        {
            if (level.name.Contains("Level "))
            {
                Button btn = (Button)level.GetComponent<Button>();

                btn.interactable = GetUnlocked(level);
            }
        }
    }

    private bool GetUnlocked(Transform level)
    {
        int levelNumber = int.Parse(level.name.Split(" ")[1]) - 1;

        return PlayerData.Instance.Levels[levelNumber].Unlocked;
    }
}
