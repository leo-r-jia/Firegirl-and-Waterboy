using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    //NOTE: Code which explicity deals with PlayFab can be found in PlayFabManager.cs

    [SerializeField] private List<TMP_InputField> fields;
    private int _fieldIndexer;

    //On start, set the username field as selected
    private void Start()
    {
        fields[0].Select();
    }

    private void Update()
    {
        //If tab key is pressed, set the next InputField in the list as selected
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (fields.Count <= _fieldIndexer)
            {
                _fieldIndexer = 0;
            }
            fields[_fieldIndexer].Select();
            _fieldIndexer++;
        }
    }
}