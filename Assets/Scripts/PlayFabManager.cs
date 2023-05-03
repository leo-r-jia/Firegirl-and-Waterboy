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

    public UnityEvent LoggedIn;
    public UnityEvent LoggedOut;

    #region Scene persistence
    public static PlayFabManager Instance;

    //As soon as created
    private void Awake()
    {
        //After first launch, destroy additional instances of PlayFabManager
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Login and Sign Up
    public void Start()
    {
        messageText.color = new Color32(231, 112, 112, 255); //Red

        //Log in the player if this canvas is loaded and they've already logged in
        if (PlayFabAuthenticationAPI.IsEntityLoggedIn())
        {
            ClearFields();
            LoggedIn.Invoke();
        }
    }

    //When the register button is clicked
    public void CreateAccountButton()
    {
        if (!ValidateInput())
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
        if (!ValidateInput())
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
    private bool ValidateInput()
    {
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

    //Clear the input fields of data
    private void ClearFields()
    {
        usernameInput.text = string.Empty;
        passwordInput.text = string.Empty;

        messageText.text = string.Empty;
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        SavePlayer();
        PlayerData.Instance.Username = usernameInput.text;
        ClearFields();
        LoggedIn.Invoke();
    }

    private void OnLoginSuccess(LoginResult result)
    {
        LoadPlayer();
    }

    private void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
    #endregion

    #region Player data
    //See OnDataRecieved
    public void LoadPlayer() {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    //Imports the player data recieved from PlayFab into the PlayerData script
    private void OnDataRecieved(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Coins") && result.Data.ContainsKey("Unlocked Levels") && result.Data.ContainsKey("High Scores"))
        {
            PlayerData.Instance.LoadPlayer(usernameInput.text, result.Data["Coins"].Value, result.Data["Unlocked Levels"].Value, result.Data["High Scores"].Value);
        }
        else
        {
            Debug.Log("Player data incomplete and could not be loaded!");
        }

        ClearFields();
        LoggedIn.Invoke();

    }

    //Save a player's data to PlayFab
    public void SavePlayer()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Coins", PlayerData.Instance.Coins.ToString() },
                { "Unlocked Levels", string.Join(',', PlayerData.Instance.LevelsUnlocked) },
                { "High Scores", string.Join(',', PlayerData.Instance.HighScores) }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    private void OnDataSend(UpdateUserDataResult result)
    {
        //Can add debug if needed
    }
    #endregion

    //Log out the player, saving their data and allowing them to sign in with another account
    public void Logout()
    {
        //Save and then reset the player
        SavePlayer();
        PlayerData.Instance.InitialisePlayer();

        //Not an API call, just clears login credentials within Unity. A player's session ticket is valid for 24 hours
        PlayFabClientAPI.ForgetAllCredentials();

        LoggedOut.Invoke();
    }
}