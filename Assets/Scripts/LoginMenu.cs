using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    //NOTE: Code which uses the PlayFab API can be found in PlayFabManager.cs

    [SerializeField] private List<TMP_InputField> fields;
    private int fieldIndexer;

    //On start, set the username field as selected
    private void Start()
    {
        fieldIndexer = 0;
        fields[fieldIndexer].Select();
        fieldIndexer++;
    }

    public void CreateAccount()
    {
        PlayFabManager.Instance.CreateAccountButton();
    }

    public void Login()
    {
        PlayFabManager.Instance.LoginButton();
    }

    private void Update()
    {
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