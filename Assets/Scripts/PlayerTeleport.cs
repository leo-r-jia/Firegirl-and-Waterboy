using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private Color dissolveColor;
    private float dissolveSpeed = 50f;
    private float appearSpeed = 5f;

    public void Dissolve()
    {
        DissolveManager dissolveManager = GetComponent<DissolveManager>();
        dissolveManager.Dissolve(dissolveSpeed, dissolveColor);
    }

    public void Appear()
    {
        DissolveManager dissolveManager = GetComponent<DissolveManager>();
        dissolveManager.Appear(appearSpeed, dissolveColor);
    }
}
