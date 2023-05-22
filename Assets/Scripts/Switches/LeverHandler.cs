using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : SwitchHandler
{
    [SerializeField] protected Transform offCheck;
    [SerializeField] private AudioSource clickSound;

    private void Start()
    {
        //Set the lever's initial states
        state = false;
        previousState = false;
    }

    private void Update()
    {
        //Check the lever's position and change its state accordingly
        if (Physics2D.OverlapCircle(onCheck.position, 0.2f, switchInteractionLayer))
        {
            state = true;
        }
        else if (Physics2D.OverlapCircle(offCheck.position, 0.2f, switchInteractionLayer))
        {
            state = false;
        }

        //If the lever's value has changed, broadcast this and update its prev. state, play click sound when switched
        if (state != previousState)
        {
            if (state)
            {
                clickSound.Play();
                switchedOn.Invoke();
            }
            else
            {
                clickSound.Play();
                switchedOff.Invoke();
            }

            previousState = state;
        }
    }

    //Return whether the switch is on
    public override bool IsOn()
    {
        return state;
    }
}