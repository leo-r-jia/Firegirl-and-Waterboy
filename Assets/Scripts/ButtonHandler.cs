using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : SwitchHandler
{
    [SerializeField] private AudioSource clickSound;
    private bool on = false;

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

        //If the button's value has changed, broadcast this and update its prev. state
        if (state != previousState)
        {
            if (state)
            {
                switchedOn.Invoke();
                on = true;
                PlaySound();
            } 
            else
            {
                switchedOff.Invoke();
                on = false;
            }
            previousState = state;
        }
    }

    //Checks if the interactable part of the switch is touching a point
    public override bool IsOn()
    {
        return state;
    }

    private void PlaySound()
    {
        if (on)
        {
            clickSound.Play();
        }

    }
}
