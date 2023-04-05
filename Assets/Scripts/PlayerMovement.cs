using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private MovementState state;

    private float dirX;
    public float speed = 12f;
    public float acceleration;
    public float decceleration;
    private float jumpAmount = 12f;
    private bool isFacingRight = true;
    private bool jumpKeyPressed;

    private void Start()
    {
        state = MovementState.Idle;
        acceleration = 5;
        decceleration = 5;
}

    //FixedUpdate is synced with Unity physics
    private void FixedUpdate()
    {
        float targetSpeed = dirX * speed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Abs(speedDif) * accelRate * Mathf.Sign(speedDif);

        //Add force to the player to move them. * by Vector2.right to only affect them in the x direction
        rb.AddForce(movement * Vector2.right);

        //Flip the player based on what direction they're facing
        if (!isFacingRight && dirX > 0f || isFacingRight && dirX < 0f)
        {
            Flip();
        }

        //If the player is trying to jump and is grounded
        if (jumpKeyPressed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
        }

        UpdateAnimationState();
    }

    //Jump method
    public void Jump(InputAction.CallbackContext context)
    {
        jumpKeyPressed = context.performed;

        //Reduce the jump amount if the player lets go of jump early
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    //Returns true if the player is touching the ground layer
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
        //Set to running if the player is not touching their controls
        state = (dirX == 0f) ? MovementState.Idle : MovementState.Running;

        if (rb.velocity.y > 1.5f)
        {
            state = MovementState.Jumping;
        }
        else if (rb.velocity.y < .1f)
        {
            state = MovementState.Falling;
        }
    }

    //Return the state of the player's movement
    public MovementState GetState()
    {
        return state;
    }
}
