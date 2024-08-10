using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]

public class Jump : MonoBehaviour
{

    [SerializeField] BoxCollider2D groundDetector;
    [SerializeField] float jumpStrength;
    [SerializeField] float maxJumpHeight;

    Rigidbody2D myRigidbody2D;

    bool isOnGround;
    bool isJumping;

    float currentJumpHeight = 0;

    private void Awake()
    {
        if (!groundDetector.isTrigger)
        {
            groundDetector.isTrigger = true;
        }

        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //begin jump
        if (isOnGround && !isJumping && Input.GetKey(KeyCode.Space))
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
