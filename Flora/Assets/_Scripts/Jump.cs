using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Jump : MonoBehaviour
{

    [SerializeField] BoxCollider2D groundDetector;
    [SerializeField] float jumpStrength;
    [SerializeField] float maxJumpHeight;
    [SerializeField] float maxFallSpeed;

    Rigidbody2D myRigidbody2D;

    bool isOnGround;
    bool isJumping;
    public bool isBouncing;

    float currentJumpHeight = 0;

    Animator myAnimator;

    private void Awake()
    {
        if (!groundDetector.isTrigger)
        {
            groundDetector.isTrigger = true;
        }

        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isBouncing)
        {
            //begin jump
            if (isOnGround && !isJumping && Input.GetKeyDown(KeyCode.Space))
            {
                StartJump();
            }

            //do jump
            if (isJumping && Input.GetKey(KeyCode.Space))
            {
                myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpStrength);
                currentJumpHeight += jumpStrength * Time.deltaTime;
            }

            //end jump if key released
            if (isJumping && !Input.GetKey(KeyCode.Space))
            {
                EndJump();
            }

            //end jump if max height is reached
            if (isJumping && currentJumpHeight >= maxJumpHeight)
            {
                EndJump();
            }

            //clamp fall speed
            if (myRigidbody2D.velocity.y <= -maxFallSpeed)
            {
                myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, -maxFallSpeed);
            }
        }
        else
        {
            currentJumpHeight += jumpStrength*2 * Time.deltaTime;
            if (isOnGround)
            {
                currentJumpHeight = 0;
                myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpStrength*2);
            }
            else
            {
                if (currentJumpHeight >= maxJumpHeight*2)
                {
                    EndJump();
                    isBouncing = false;
                    Debug.Log("End Jump");
                }
            }
        }

        myAnimator.SetBool("IsOnGround", isOnGround);
        myAnimator.SetBool("IsJumping", isJumping);
    }

    private void StartJump()
    {
        isJumping = true;
        currentJumpHeight = 0;
    }

    private void EndJump()
    {
        isJumping = false;
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
