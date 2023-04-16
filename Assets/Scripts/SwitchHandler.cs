using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SwitchHandler : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D interactableRB;
    [SerializeField] protected Transform onCheck;
    [SerializeField] protected LayerMask switchInteractionLayer;

    protected bool state, previousState;

    public UnityEvent switchedOn;
    public UnityEvent switchedOff;

    //Checks if the interactable part of the switch is touching a point
    public abstract bool IsOn();
}
