using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Jump : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] BoxCollider2D groundDetector;
    [Space]

    [SerializeField] float jumpStrength;
    [SerializeField] float maxJumpHeight;

    [SerializeField] float jumpGravityScale = 3.5f;
    [Space]

    [SerializeField] float jumpHangVelocityThreshold = .1f;
    [SerializeField] float jumpHangGravityMultiplier = .5f;
    [Space]

    [SerializeField] float maxFallSpeed;

    [Header("DEBUG")]
    public bool isOnGround;
    public bool isJumping;

    public AudioSource jumpSound;
    public AudioSource landSound;

    float currentJumpHeight = 0;

    Rigidbody2D myRigidbody2D;

    Animator myAnimator;

    PlayerControls playerControls;

    private void Awake()
    {
        if (!groundDetector.isTrigger)
        {
            groundDetector.isTrigger = true;
        }

        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.gravityScale = jumpGravityScale;

        myAnimator = GetComponent<Animator>();

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void OnJumpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //begin jump
            if (isOnGround && !isJumping)
            {
                StartJump();
            }

        }

        if (context.canceled)
        {
            //end jump if key released
            if (isJumping)
            {
                EndJump();
            }
        }

    }

    private void FixedUpdate()
    {

        //do jump
        if (isJumping)
        {
            ProcessJump();
        }

        //reduce gravity at peak of jump (to make it feel more floaty)
        ProcessJumpHang();

        //clamp fall speed
        ClampFallSpeed();

        myAnimator.SetBool("IsOnGround", isOnGround);
        myAnimator.SetBool("IsJumping", isJumping);

    }


    private void StartJump()
    {
        isJumping = true;
        currentJumpHeight = 0;
        jumpSound.Play();
    }

    public void EndJump()
    {
        isJumping = false;
    }

    
    private void ProcessJump()
    {
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpStrength);
        currentJumpHeight += jumpStrength * Time.deltaTime;

        //end jump if jump height is reached
        if (currentJumpHeight >= maxJumpHeight)
        {
            EndJump();
        }
    }

    private void ProcessJumpHang()
    {
        if (Mathf.Abs(myRigidbody2D.velocity.y) <= jumpHangVelocityThreshold)
        {
            myRigidbody2D.gravityScale = jumpGravityScale * jumpHangGravityMultiplier;
        }
        else
        {
            myRigidbody2D.gravityScale = jumpGravityScale;
        }
    }
    private void ClampFallSpeed()
    {
        if (myRigidbody2D.velocity.y <= -maxFallSpeed)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, -maxFallSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isOnGround = true;
            landSound.Play();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isOnGround = false;
        }
    }

    

}
