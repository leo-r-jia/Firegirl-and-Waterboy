using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text UsernameText;
    [SerializeField] private UnityEngine.UI.Button LogOutButton;
    [SerializeField] private UnityEngine.UI.Button ProfileButton;

    //Whenever object becomes active
    void OnEnable()
    {
        if (PlayerData.Instance != null && PlayerData.Instance.Username != null)
        {
            UsernameText.text = PlayerData.Instance.Username;

            LogOutButton.gameObject.SetActive(true);
            ProfileButton.gameObject.SetActive(false);
        }
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