using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    MovementState state;

    private float dirX;
    private float speed = 12f;
    private float jumpAmount = 12f;
    private float momentumFactor = 0.8f;
    private bool isFacingRight = true;

    private void Start()
    {
        state = MovementState.Idle;
    }

    //Update is called once per frame
    private void Update()
    {
        //Flip the player based on what direction they're facing
        if (!isFacingRight && dirX > 0f || isFacingRight && dirX < 0f)
        {
            Flip();
        }
    }

    //FixedUpdate is synced with Unity physics
    private void FixedUpdate()
    {
        //If the player is trying to move and meets the conditions, set their velocity
        if (dirX != 0 && (IsMovingInDirectionFacing() || HasAlmostStopped(rb.velocity.x)))
        {
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        }
        //If no key is pressed, decrease the player's velocity over time
        else
        {
            rb.velocity = new Vector2(rb.velocity.x * momentumFactor, rb.velocity.y);
        }

        UpdateAnimationState();
    }

    //Jump method
    public void Jump(InputAction.CallbackContext context)
    {
        //Jump if the jump key is pressed
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
        }

        //Reduce the jump amount if the player let go of jump early
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (context.performed)
        {
            Debug.Log("test");
        }
    }

    //Returns true if the player is touching the ground layer
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Return true if the player's velocity is in the direction they're facing
    private bool IsMovingInDirectionFacing()
    {
        return (isFacingRight && rb.velocity.x > 0) || (!isFacingRight && rb.velocity.x < 0);
    }

    //Return true if the player velocity has decreased below a certain magnitude
    private bool HasAlmostStopped(float velocity)
    {
        return velocity > -1f && velocity < 1f;
    }

    //Record the player's x velocity whenever their horizontal movement keys are pressed
    public void Move(InputAction.CallbackContext context)
    {
        dirX = context.ReadValue<Vector2>().x;
    }

    //Flips the player's sprite
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    //Update the player's state so that appropriate animations can be played
    private void UpdateAnimationState()
    {
        if (!HasAlmostStopped(dirX))
        {
            state = MovementState.Running;
        }
        else if (HasAlmostStopped(dirX)) {
            state = MovementState.Idle;
        }

        if (rb.velocity.y > 1.5f)
        {
            state = MovementState.Jumping;
        }
        else if (rb.velocity.y < .1f)
        {
            state = MovementState.Falling;
        }
    }
}
