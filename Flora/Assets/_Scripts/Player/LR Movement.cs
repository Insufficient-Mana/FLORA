using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class LRMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D myRigidbody2D;
    SpriteRenderer mySpriteRenderer;
    Animator myAnimator;
    public bool moving;
    public bool canMove;

    PlayerControls playerControls;


    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        canMove = true;

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

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("cast"))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        

        if (canMove)
        {
            //move player based on LR input
            float moveDirection = playerControls.Gameplay.Move.ReadValue<float>();
            myRigidbody2D.velocity = new Vector2(moveDirection * moveSpeed, myRigidbody2D.velocity.y);

            //this is used for the public bool that is read by other scripts
            if (moveDirection == 0)
            {
                moving = false;
            }
            else
            {
                moving = true;
            }





            //set sprite to face the direction the player is moving
            if (moveDirection > 0)
            {
                mySpriteRenderer.flipX = false;
            }
            else if (moveDirection < 0)
            {
                mySpriteRenderer.flipX = true;
            }

            //this is used to tell the animator if the player is moving
            myAnimator.SetInteger("MoveDirection", (int)moveDirection);
        }
        
    }

}
