using System;
using UnityEngine;

public class DelayedButtonHandler : SwitchHandler
{
    [SerializeField] private float delay;
    private float elapsedTime;
    private Transform player1;
    private Transform player2;
    GameObject[] boxes;

    [SerializeField] Collider2D switchTriggerColiider;

    //Set the buttons' initial states and set player ground checking objects
    void Start()
    {
        if (delay <= 0f) {
            throw new ArgumentException("delay must be positive.");
        }

        switchIsPressed = false;
        switchPreviouslyOn = false;
        switchTriggerPointRadius = .2f;

        player1 = GameObject.Find("P1 Ground Check").transform;
        player2 = GameObject.Find("P2 Ground Check").transform;

        if (player1 == null || player2 == null)
        {
            throw new ArgumentException("Could not locate one or more player ground checks.");
        }

        boxes = GameObject.FindGameObjectsWithTag("Box");
    }

    void Update()
    {
        SetState();

        //If the button's value has changed
        if (switchIsPressed != switchPreviouslyOn)
        {
            BroadcastState();

            switchPreviouslyOn = switchIsPressed;
        }

        if (switchIsPressed)
        {
            WhilePressed();
        }
    }

    //Check and set whether the button is on and freeze it in place if so
    void SetState()
    {
        if ((PlayerIsTouching() || BoxIsTouching()) && Physics2D.OverlapCircle(onCheck.position, switchTriggerPointRadius, switchInteractionLayer))
        {
            switchIsPressed = true;

            //Set the starting time to now and freeze the button
            interactableRB.constraints = RigidbodyConstraints2D.FreezeAll;
            elapsedTime = Time.deltaTime;
        }
        else if (!Physics2D.OverlapCircle(onCheck.position, switchTriggerPointRadius, switchInteractionLayer))
        {
            switchIsPressed = false;
        }
    }

    //Increment the elapsed pressed time and unfreeze switch if time is up
    void WhilePressed()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > delay)
        {
            //Allow rb to move along y (remove y constraint from bitwise constraints enum)
            interactableRB.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

            //Fix button staying stuck in place
            Vector3 upwardsVector = new(0f, 1f, 0f);
            interactableRB.velocity = upwardsVector;
        }
    }

    //Check if a player is touching the switch
    bool PlayerIsTouching()
    {
        float playerGroundPointRadius = .3f;
        
        return (Physics2D.OverlapCircle(player1.position, playerGroundPointRadius, switchInteractionLayer) || Physics2D.OverlapCircle(player2.position, playerGroundPointRadius, switchInteractionLayer));
    }

    //Check if a box is touching the switch
    bool BoxIsTouching()
    {
        foreach (GameObject box in boxes)
        {
            if (Physics2D.IsTouching(box.GetComponent<BoxCollider2D>(), switchTriggerColiider))
                return true;
        }

        return false;
    }

    //Broadcast the button's switchIsPressed
    void BroadcastState()
    {
        if (switchIsPressed)
        {
            AudioManager.Instance.PlaySFX("Switch");

            switchedOn.Invoke();
        }
        else
        {
            switchedOff.Invoke();
        }
    }

    //Whether the button is on
    public override bool IsOn()
    {
        return switchIsPressed;
    }
}
