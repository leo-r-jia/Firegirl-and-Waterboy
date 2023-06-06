using UnityEngine;

public class LeverHandler : SwitchHandler
{
    [SerializeField] protected Transform offCheck;

    void Start()
    {
        //Set the lever's initial states
        switchIsPressed = false;
        switchPreviouslyOn = false;

        switchTriggerPointRadius = .2f;
    }

    void Update()
    {
        //Check the lever's position and change whether its pressed accordingly
        if (Physics2D.OverlapCircle(onCheck.position, switchTriggerPointRadius, switchInteractionLayer))
        {
            switchIsPressed = true;
        }
        else if (Physics2D.OverlapCircle(offCheck.position, switchTriggerPointRadius, switchInteractionLayer))
        {
            switchIsPressed = false;
        }

        if (switchIsPressed != switchPreviouslyOn)
        {
            LeverChangedState();
        }
    }

    //If the lever's value has changed, broadcast this and update its prev. switchIsPressed, play click sound when switched
    void LeverChangedState()
    {
        if (switchIsPressed)
        {
            switchedOn.Invoke();
        }
        else
        {
            switchedOff.Invoke();
        }

        AudioManager.Instance.PlaySFX("Switch");

        switchPreviouslyOn = switchIsPressed;
    }

    //Return whether the lever is on
    public override bool IsOn()
    {
        return switchIsPressed;
    }
}
