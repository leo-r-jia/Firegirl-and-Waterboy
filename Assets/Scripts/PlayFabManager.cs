using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor.PackageManager;
using UnityEngine.UI;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    public TMP_Text messageText;
    public TMP_InputField emailInput, passwordInput;
    private Color red = new Color32(231, 112, 112, 255);
    private Color green = new Color32(112, 231, 115, 255);

    //When the register button is clicked
    public void RegisterButton()
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.color = red;
            messageText.text = "Password must be at least 6 characters";
            return;
        }

        //Form request
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };

        //Send register request
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    //When the login button is clicked
    public void LoginButton()
    {
        //Form request
        var request = new LoginWithEmailAddressRequest 
        { 
            Email = emailInput.text,
            Password = passwordInput.text
        };

        //Send login request
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    //When the reset password button is clicked
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "AB628"
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.color = green;
        messageText.text = "Successfully registered and logged in!";
    }

    private void OnLoginSuccess(LoginResult result)
    {
        messageText.color = green;
        messageText.text = "Successfully logged in!";
    }

    private void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.color = green;
        messageText.text = "Password reset email sent";
    }

    private void OnError(PlayFabError error)
    {
        messageText.color = red;
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
}