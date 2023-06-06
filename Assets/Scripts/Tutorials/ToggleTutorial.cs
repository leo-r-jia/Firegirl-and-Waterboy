using UnityEngine;

public class ToggleTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorial;

    //Toggle the tutorial visiblity when a menu appears
    public void ToggleTutorialVisibility()
    {
        tutorial.SetActive(!tutorial.activeSelf);
    }
}
