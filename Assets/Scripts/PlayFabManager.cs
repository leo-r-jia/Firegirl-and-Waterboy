using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor.PackageManager;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEditor;
using System;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_InputField usernameInput, passwordInput;
    private Color red = new Color32(231, 112, 112, 255);
    private Color green = new Color32(112, 231, 115, 255);
    [SerializeField] private PlayerData playerData;

    public UnityEvent LoggedIn;

    #region Login and Sign Up
    public void Start()
    {
        //Log in the player if this canvas is loaded and they've already logged in
        if (PlayFabAuthenticationAPI.IsEntityLoggedIn())
        {
            LoggedIn.Invoke();
        }
    }

    //When the register button is clicked
    public void CreateAccountButton()
    {
        if (!validateInput())
        {
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
        if (!validateInput())
        {
            return;
        }

        //Form request for login with username + password
        var request = new LoginWithPlayFabRequest
        {
            Username = usernameInput.text,
            Password = passwordInput.text
        };

        //Send login request
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
    }

    //Perform basic input validation. Returns true if all checks pass
    private bool validateInput()
    {
        messageText.color = red;

        if (usernameInput.text.Length < 1)
        {
            messageText.text = "Please enter a username";
            return false;
        }
        else if (passwordInput.text.Length < 1)
        {
            messageText.text = "Please enter a password";
            return false;
        }
        else if (passwordInput.text.Length < 6)
        {
            messageText.text = "Password must be at least 6 characters";
            return false;
        }

        return true;
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        SavePlayer();
        LoggedIn.Invoke();
    }

    private void OnLoginSuccess(LoginResult result)
    {
        SavePlayer();
        LoggedIn.Invoke();
    }

    private void OnError(PlayFabError error)
    {
        messageText.color = red;
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
    #endregion

    #region Player data
    public void LoadPlayer() {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    //Save a player's data to PlayFab
    public void SavePlayer()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Gems", playerData.Gems.ToString() },
                { "Unlocked Levels", string.Join(',', playerData.LevelsUnlocked) },
                { "High Scores", string.Join(',', playerData.HighScores) }
    }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    private void OnDataSend(UpdateUserDataResult result)
    {
        //Can add debug if needed
    }

    //Imports the player data recieved from PlayFab into the PlayerData script
    private void OnDataRecieved(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Gems") && result.Data.ContainsKey("Unlocked Levels") && result.Data.ContainsKey("High Scores"))
        {
            playerData.LoadPlayer(result.Data["Gems"].Value, result.Data["Unlocked Levels"].Value, result.Data["High Scores"].Value);
        }
        else
        {
            Debug.Log("Player data incomplete and could not be loaded!");
        }
    }

    #endregion
}