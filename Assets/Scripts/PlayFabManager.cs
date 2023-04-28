using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor.PackageManager;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_InputField usernameInput, passwordInput;
    private Color red = new Color32(231, 112, 112, 255);
    private Color green = new Color32(112, 231, 115, 255);

    public UnityEvent LoggedIn;

    /*public void Start()
    {
        var request = new GetAccountInfoRequest
        {

        }

        Debug.Log(PlayFabClientAPI.getAcc);
    }*/

    //When the register button is clicked
    public void CreateAccountButton()
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.color = red;
            messageText.text = "Password must be at least 6 characters";
            return;
        }

        //Form request for registration with username + password
        var request = new RegisterPlayFabUserRequest
        {
            Username = usernameInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };

        //Send register request
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    //When the login button is clicked
    public void LoginButton()
    {
        //Form request for login with username + password
        var request = new LoginWithPlayFabRequest 
        { 
            Username = usernameInput.text,
            Password = passwordInput.text
        };

        //Send login request
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        LoggedIn.Invoke();
    }

    private void OnLoginSuccess(LoginResult result)
    {
        LoggedIn.Invoke();
    }

    private void OnError(PlayFabError error)
    {
        messageText.color = red;
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
}