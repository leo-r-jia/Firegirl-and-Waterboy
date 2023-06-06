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
    [SerializeField] private LayerMask switchTriggerLayer;
    [SerializeField] private LayerMask boxLayer;

    //For animator and animations
    private MovementState state;
    private Animator anim;

    [SerializeField] private float maxSpeed = 12f;
    [SerializeField] private float jumpAmount = 15f;
    private float acceleration = 6f;
    private float decceleration = 6f;
    private bool isFacingRight = true;
    private bool jumpKeyPressed;
    private float dirX;

    //For sound effects
    private AudioSource jumpSoundEffect;
    private AudioSource landSoundEffect;
    private AudioSource runSoundEffect;
    private AudioSource landInWaterSoundEffect;
    private AudioSource runInWaterSoundEffect;

    private bool touchingLiquid = false;
    private bool inWater = false;
    private AudioSource currentRunningSound;

    //Before first frame update
    private void Start()
    {
        state = MovementState.Idle;
        anim = GetComponent<Animator>();
        InputSystem.EnableDevice(Keyboard.current);

        //Get sounds from the sound manager
        jumpSoundEffect = GetSoundForThisPlayer("Jump");
        landSoundEffect = GetSoundForThisPlayer("Land");
        runSoundEffect = GetSoundForThisPlayer("Run");
        runInWaterSoundEffect = GetSoundForThisPlayer("Run In Water");
        landInWaterSoundEffect = GetSoundForThisPlayer("Land In Water");
        currentRunningSound = runSoundEffect;
    }

    AudioSource GetSoundForThisPlayer(string name)
    {
        AudioSource sound;

        if (gameObject.name == "Player 1")
            sound = AudioManager.Instance.GetSound(name + " P1").source;
        else
            sound = AudioManager.Instance.GetSound(name + " P2").source;

        return sound;
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

        SoundEffectManager();

        UpdateAnimationState();
    }

    // Set the flag when the player touches water
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fire") || collision.gameObject.CompareTag("Water"))
        {
            touchingLiquid = true;
        }
        else
        {
            touchingLiquid = false;
        }
    }

    //Triggers sound effects depending on player state
    private void SoundEffectManager()
    {
        bool falling = false;

        if (touchingLiquid && !inWater)
        {
            currentRunningSound = runInWaterSoundEffect;
            EaseOutSound(runSoundEffect, 2f);
            inWater = true;
        } 
        else if (!touchingLiquid && inWater)
        {
            currentRunningSound = runSoundEffect;
            EaseOutSound(runInWaterSoundEffect, 4f);
            inWater = false;
        }

        //If player is running on the ground, play running sound effect
        if (IsGrounded() && GetState() == MovementState.Running)
        {
            currentRunningSound.enabled = true;
        }
        else if (currentRunningSound.enabled)
        {
            if (inWater)
                EaseOutSound(currentRunningSound, 4f);
            else
                EaseOutSound(currentRunningSound, 2f);
        }

        //If falling, play landing sound upon landing
        if (GetState() == MovementState.Falling)
        {
            falling = true;
        }

        if (IsGrounded() && falling && !Physics2D.OverlapCircle(groundCheck.position, 0.2f, switchTriggerLayer) && !landSoundEffect.isPlaying)
        {
            StartCoroutine(PlayLandSound());
            falling = false;
        }

        //If jumping from ground, play jump sound effect
        if (jumpKeyPressed && IsGrounded())
        {
            jumpSoundEffect.Play();
        } 
    }

    private IEnumerator PlayLandSound()
    {
        yield return new WaitForSeconds(0.05f); // Adjust the delay as needed

        if (touchingLiquid)
        {
            landInWaterSoundEffect.Play();
        }
        else
        {
            landSoundEffect.Play();
        }
    }

    //Ease out sound
    private void EaseOutSound(AudioSource audio, float fadeTime)
    {
        if (audio.enabled)
        {
            float startVolume = audio.volume;
            float timer = 0f;

            while (timer < fadeTime)
            {
                Debug.Log("Fading " + fadeTime + timer);
                timer += Time.deltaTime;
                audio.volume = Mathf.Lerp(startVolume, 0f, timer / fadeTime);
            }

            audio.enabled = false;
            audio.volume = startVolume;
        }
    }

    //Stop all player sound effects playing
    public void StopSoundEffects()
    {
        jumpSoundEffect.Stop();
        landSoundEffect.Stop();
        runSoundEffect.Stop();
        runInWaterSoundEffect.Stop();
    }

    //Record the player's x velocity whenever their horizontal movement keys are pressed
    public void Move(InputAction.CallbackContext context)
    {
        dirX = context.ReadValue<Vector2>().x;
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

    //Returns true if the player is touching a layer that they can jump on
    public bool IsGrounded()
    {
        return PlayerIsOnLayer(groundLayer) || PlayerIsOnLayer(switchTriggerLayer) || PlayerIsOnLayer(boxLayer);
    }

    bool PlayerIsOnLayer(LayerMask layer)
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, layer);
    }

    //Calculate the and return the player's movement speed
    private float CalculateMovement()
    {
        //Find the directional (+/-) maxSpeed the player can move
        float dirMaxSpeed;

        //If in water, max speed is 80% 
        if (touchingLiquid) 
            dirMaxSpeed = dirX * maxSpeed * 0.8f;
        else 
            dirMaxSpeed = dirX * maxSpeed;

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
        transform.Rotate(0, -180, 0);
    }

    //Update the player's state so that appropriate animations can be played
    private void UpdateAnimationState()
    {
        //Set to idle if the player is not touching their controls
        state = (dirX == 0f) ? MovementState.Idle : MovementState.Running;

        if (rb.velocity.y > 1.5f)
        {
            state = MovementState.Jumping;
        }
        else if (rb.velocity.y < -1.6f)
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
