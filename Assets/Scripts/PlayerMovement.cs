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
    private readonly float acceleration = 6f;
    private readonly float decceleration = 6f;
    private bool isFacingRight = true;
    private bool jumpKeyPressed;
    private float dirX;

    //For sound effects
    private AudioSource jumpSoundEffect;
    private AudioSource landSoundEffect;
    private AudioSource runSoundEffect;

    private bool touchingLiquid = false;

    //Before first frame update
    private void Start()
    {
        state = MovementState.Idle;
        anim = GetComponent<Animator>();
        InputSystem.EnableDevice(Keyboard.current);

        jumpSoundEffect = GetSoundForThisPlayer("Jump");
        landSoundEffect = GetSoundForThisPlayer("Land");
        runSoundEffect = GetSoundForThisPlayer("Run");
    }

    //Sounds are different dependant on whether the object is Player 1 or 2
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
        //Add force to the player to move them, though only in the x direction
        rb.AddForce(CalculateMovement() * Vector2.right);

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

        //If player is running on the ground, play running sound effect
        if (IsGrounded() && GetState() == MovementState.Running)
        {
            runSoundEffect.enabled = true;
        }
        else if (runSoundEffect.enabled)
        {
            //Ease out running/footstep sound
            float currentTime = 0;
            float start = runSoundEffect.volume;

            while (currentTime < (float)3500)
            {
                currentTime += Time.deltaTime;
                runSoundEffect.volume = Mathf.Lerp(start, 0, currentTime / (float)3500);
            }

            runSoundEffect.enabled = false;
            runSoundEffect.volume = start;
        }

        //If falling, play landing sound upon landing
        if (GetState() == MovementState.Falling)
        {
            falling = true;
        }

        if (IsGrounded() && falling && !Physics2D.OverlapCircle(groundCheck.position, 0.2f, switchTriggerLayer) && !landSoundEffect.isPlaying)
        {
            landSoundEffect.Play();
            falling = false;
        }

        //If jumping from ground, play jump sound effect
        if (jumpKeyPressed && IsGrounded())
        {
            jumpSoundEffect.Play();
        }
    }

    //Stop all player sound effects playing
    public void StopSoundEffects()
    {
        jumpSoundEffect.Stop();
        landSoundEffect.Stop();
        runSoundEffect.Stop();
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
        float dirMaxSpeed = dirX * maxSpeed;
        if (touchingLiquid)
            dirMaxSpeed = dirMaxSpeed * 0.7f;

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
