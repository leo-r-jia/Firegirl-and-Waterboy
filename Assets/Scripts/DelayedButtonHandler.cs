using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedButtonHandler : SwitchHandler
{
    [SerializeField] private float delay;
    private float elapsedTime;
    private Transform player1;
    private Transform player2;

    [SerializeField] private AudioSource clickSound;

    //Set the button's initial states and set player ground checking objects
    public void Start()
    {
        state = false;
        previousState = false;

        player1 = GameObject.Find("P1 Ground Check").transform;
        player2 = GameObject.Find("P2 Ground Check").transform;
    }

    public void Update()
    {
        //Check and set the button's state
        if (PlayerIsTouching() && Physics2D.OverlapCircle(onCheck.position, 0.2f, switchInteractionLayer))
        {
            state = true;

            //Set the starting time to now and freeze the top of the button (so that it stays on)
            interactableRB.constraints = RigidbodyConstraints2D.FreezeAll;
            elapsedTime = Time.deltaTime;
        }
        else if (!Physics2D.OverlapCircle(onCheck.position, 0.2f, switchInteractionLayer))
        {
            state = false;
        }

        //If the button's value has changed
        if (state != previousState)
        {
            BroadcastState();

            previousState = state;
        }

        //If the button is on
        if (state)
        {
            //Increment time since last on
            elapsedTime += Time.deltaTime;

            //If the delay is up, release the button
            if (elapsedTime > delay)
            {
                //Allow rb to move along y (remove y constraint from bitwise constraints enum)
                interactableRB.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

                //Fix button staying stuck in place
                interactableRB.velocity = new Vector3(0f, 1f, 0f);
            }
        }
    }

    //Check if a player is touching the button
    private bool PlayerIsTouching()
    {
        return (Physics2D.OverlapCircle(player1.position, 0.3f, switchInteractionLayer) || Physics2D.OverlapCircle(player2.position, 0.3f, switchInteractionLayer));
    }

    //Broadcast the button's state
    private void BroadcastState()
    {
        if (state)
        {
            //Play click sound
            clickSound.Play();

            //Broadcast the button is on
            switchedOn.Invoke();
        }
        else
        {
            //Broadcast the button is off
            switchedOff.Invoke();
        }
    }

    //Checks if the interactable part of the switch is touching a point
    public override bool IsOn()
    {
        return state;
    }
}
