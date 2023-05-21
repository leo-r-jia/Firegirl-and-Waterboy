using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField] private bool doLevelLocking = false;

    public void OnEnable()
    {
        if (!doLevelLocking) return;

        //For every level button, set its state
        foreach (Transform level in transform.GetComponentsInChildren<Transform>())
        {
            if (level.name.ContainsInsensitive("level "))
            {
                Button btn = (Button) level.GetComponent<Button>();

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
