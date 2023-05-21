using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.Events;


//WARNING: Do not reference this script by dragging and dropping into a gameobject. Issues will arise
public class PlayFabManager : MonoBehaviour
{
    public UnityEvent LoggedIn;
    public UnityEvent LoggedOut;

    public string LocalUsername { get; private set; }
    public string ErrorText { get; private set; }

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
    //When the register button is clicked
    public void CreateAccount(string username, string password)
    {
        ErrorText = null;
        LocalUsername = username;

        //Form request for registration with username + password
        var request = new RegisterPlayFabUserRequest
        {
            Username = username,
            Password = password,
            RequireBothUsernameAndEmail = false
        };

        //Send register request
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    //When the login button is clicked
    public void Login(string username, string password)
    {
        ErrorText = null;
        LocalUsername = username;

        //Form request for login with username + password
        var request = new LoginWithPlayFabRequest
        {
            Username = username,
            Password = password
        };

        //Send login request
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
    }

    //On successfully registering, save the player's data and set their username
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        PlayerData.Instance.Username = LocalUsername;
        SavePlayer();
        LoggedIn.Invoke();
    }

    //Load the player if they successfully log in
    private void OnLoginSuccess(LoginResult result)
    {
        LoadPlayer();
    }

    //If an error occurs, update the user and also print a detailed report to the console
    private void OnError(PlayFabError error)
    {
        ErrorText = error.ErrorMessage;
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
        if (result.Data != null && result.Data.ContainsKey("Unlocked Levels") && result.Data.ContainsKey("Level Scores"))
        {
            PlayerData.Instance.LoadPlayer(LocalUsername, result.Data["Unlocked Levels"].Value, result.Data["Level Scores"].Value);

            LoggedIn.Invoke();
        }
        else
        {
            //Player data format has changed/a new level has been added
            Debug.Log("Player data incomplete and could not be loaded!");
        }
    }

    //Save a player's data to PlayFab
    public void SavePlayer()
    {
        if (PlayerData.Instance.Username != null)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { "Unlocked Levels", PlayerData.Instance.UnlockedLevelsToString() },
                    { "Level Scores", PlayerData.Instance.ScoresToString() }
                }
            };

            PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        }
    }

    private void OnDataSend(UpdateUserDataResult result)
    {
        //Can add debug if needed
    }

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

    #endregion
}