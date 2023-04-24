using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private MovementState state;
    private Animator anim;

    private float maxSpeed = 12f;
    private float jumpAmount = 12f;
    private float acceleration = 6f;
    private float decceleration = 6f;
    private bool isFacingRight = true;
    private bool jumpKeyPressed;
    private float dirX;

    //Before first frame update
    private void Start()
    {
        state = MovementState.Idle;
        anim = GetComponent<Animator>();
    }

    //FixedUpdate is synced with Unity physics
    private void FixedUpdate()
    {
        //Add force to the player to move them. * by Vector2.right to only affect them in the x direction
        rb.AddForce(CalculateMovement() * Vector2.right);
        
        //If the player is trying to jump and is grounded
        if (jumpKeyPressed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
            rb.AddForce(jumpAmount * Vector2.down);
        }

        //Flip the player based on what direction they're facing
        if (!isFacingRight && dirX > 0f || isFacingRight && dirX < 0f)
        {
            Flip();
        }

        UpdateAnimationState();
    }

    //Record the player's x velocity whenever their horizontal movement keys are pressed
    private void Move(InputAction.CallbackContext context)
    {
        dirX = context.ReadValue<Vector2>().x;
    }

    //Jump method
    private void Jump(InputAction.CallbackContext context)
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

    //Calculate the and return the player's movement speed
    private float CalculateMovement()
    {
        //Find the directional (+/-) maxSpeed the player can move
        float dirMaxSpeed = dirX * maxSpeed;

        //Calc. the difference between their current speed and their maxSpeed
        float speedDif = dirMaxSpeed - rb.velocity.x;

        //Set the acceleration based on what direction they're moving
        float accelRate = (Mathf.Abs(dirMaxSpeed) > 0.01f) ? acceleration : decceleration;

        //Calculate and return their new movement speed

        return Mathf.Abs(speedDif) * accelRate * Mathf.Sign(speedDif);
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
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.Falling;
        }

        anim.SetInteger("state", (int)state);

    }

    //Return the state of the player's movement
    public MovementState GetState()
    {
        return state;
    }
}
