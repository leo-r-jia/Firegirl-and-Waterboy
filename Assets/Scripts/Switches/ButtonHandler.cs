using UnityEngine;

public class ButtonHandler : SwitchHandler
{
    //Set button's initial states
    void Start()
    {
        switchIsPressed = false;
        switchPreviouslyOn = false;

        switchTriggerPointRadius = .2f;
    }

    void Update()
    {
        //Check and set the button's switchIsPressed
        switchIsPressed = (Physics2D.OverlapCircle(onCheck.position, switchTriggerPointRadius, switchInteractionLayer) ? true : false);

        if (switchIsPressed != switchPreviouslyOn)
        {
            ButtonChangedState();
        }
    }

    //If the button's value has changed, broadcast this and update its prev. switchIsPressed
    void ButtonChangedState()
    {
        if (switchIsPressed)
        {
            switchedOn.Invoke();

            AudioManager.Instance.PlaySFX("Switch");
        }
        else
        {
            switchedOff.Invoke();
        }
        switchPreviouslyOn = switchIsPressed;
    }

    //Whether the button is on
    public override bool IsOn()
    {
        return switchIsPressed;
    }
}
