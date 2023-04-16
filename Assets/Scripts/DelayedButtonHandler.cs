using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedButtonHandler : SwitchHandler
{
    //Set button's initial states
    public void Start()
    {
        state = false;
        previousState = false;
    }

    public void Update()
    {
        //Check and set the button's state
        state = (Physics2D.OverlapCircle(onCheck.position, 0.2f, switchInteractionLayer) ? true : false);

        //If the button's value has changed, broadcast this
        if (state != previousState)
        {
            onTrigger.Invoke();
            previousState = state;
        }
    }

    //Checks if the interactable part of the switch is touching a point
    public override bool IsOn()
    {
        return state;
    }
}
