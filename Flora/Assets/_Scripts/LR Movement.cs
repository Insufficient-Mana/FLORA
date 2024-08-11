using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            int moveDirection = 0;

            if (Input.GetKey(KeyCode.A))
            {
                moveDirection -= 1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveDirection += 1;
            }

            if (moveDirection == 0)
            {
                moving = false;
            }
            else
            {
                moving = true;
            }

            myRigidbody2D.velocity = new Vector2(moveDirection * moveSpeed, myRigidbody2D.velocity.y);




            //set sprite to face the direction the player is moving
            if (moveDirection > 0)
            {
                mySpriteRenderer.flipX = false;
            }
            else if (moveDirection < 0)
            {
                mySpriteRenderer.flipX = true;
            }





            myAnimator.SetInteger("MoveDirection", moveDirection);
        }
        
    }

}
