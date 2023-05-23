using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private UnityEngine.UI.Button logOutButton;
    [SerializeField] private UnityEngine.UI.Button profileButton;

    //Whenever object becomes active
    void OnEnable()
    {
        if (PlayerData.Instance != null && PlayerData.Instance.Username != null)
        {
            usernameText.text = PlayerData.Instance.Username;

            logOutButton.gameObject.SetActive(true);
            profileButton.gameObject.SetActive(false);
        }
    }

    public void DoGuestLogin()
    {
        PlayFabManager.Instance.LoginAsGuest();
    }

    public void Logout()
    {
        PlayFabManager.Instance.Logout();
    }

    //Exit the game
    public void Quit()
    {
        Application.Quit();
    }
}