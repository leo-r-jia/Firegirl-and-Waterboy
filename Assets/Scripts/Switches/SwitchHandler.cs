using UnityEngine;
using UnityEngine.Events;

public abstract class SwitchHandler : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D interactableRB;
    [SerializeField] protected Transform onCheck;
    [SerializeField] protected LayerMask switchInteractionLayer;

    protected float switchTriggerPointRadius;

    protected bool switchIsPressed, switchPreviouslyOn;

    public UnityEvent switchedOn;
    public UnityEvent switchedOff;

    //Checks if the interactable part of the switch is touching a point
    public abstract bool IsOn();
}
