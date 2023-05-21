using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_InputField usernameInput, passwordInput;
    [SerializeField] private List<TMP_InputField> fields;
    private int fieldIndexer;

    public UnityEvent LoggedIn;
    public UnityEvent LoggedOut;

    private void Start()
    {
        ResetTabbing();
    }

    private void OnEnable()
    {
        PlayFabManager.Instance.LoggedIn.RemoveAllListeners();
        PlayFabManager.Instance.LoggedOut.RemoveAllListeners();
        PlayFabManager.Instance.LoggedIn.AddListener(LoginSuccessful);
        PlayFabManager.Instance.LoggedOut.AddListener(LogoutSuccessful);
    }

    //Set tabbing back to the Username field
    private void ResetTabbing()
    {
        fieldIndexer = 0;
        fields[fieldIndexer].Select();
        fieldIndexer++;
    }

    public void CreateAccount()
    {
        if (!ValidateInput()) return;

        PlayFabManager.Instance.CreateAccount(usernameInput.text, passwordInput.text);
    }

    public void Login()
    {
        if (!ValidateInput()) return;

        PlayFabManager.Instance.Login(usernameInput.text, passwordInput.text);
    }

    private void LoginSuccessful()
    {
        ClearFields();
        ResetTabbing();
        LoggedIn.Invoke();
    }

    private void LogoutSuccessful()
    {
        ClearFields();
        ResetTabbing();
        LoggedOut.Invoke();
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

    private void Update()
    {
        if (PlayFabManager.Instance.ErrorText != null)
        {
            messageText.text = PlayFabManager.Instance.ErrorText;
        }

        //If tab key is pressed, set the next InputField in the list as selected
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (fields.Count <= fieldIndexer)
            {
                fieldIndexer = 0;
            }

            fields[fieldIndexer].Select();
            fieldIndexer++;
        }
    }
}