using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingCooldown : MonoBehaviour
{
    private static TeleportingCooldown _instance;
    public static TeleportingCooldown Instance { get { return _instance; } }
    public bool teleporting = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

}

