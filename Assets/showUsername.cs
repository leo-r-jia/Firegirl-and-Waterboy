using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showUsername : MonoBehaviour
{
    //Set the username of this object to the passed string
    public void setUserName(string username)
    {
        GetComponent<TMP_Text>().text = username;
    }
}
