using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFlower : MonoBehaviour
{
    [Header("Spring Flower Info")]
    public Animator[] myAnimatorList;
    public PlatformDecay decayScript;
    bool isDecayed = false;
    public bool bounced;

    #region Awake Start and Update
    private void Awake()
    {
        decayScript = GetComponent<PlatformDecay>();
    }

    private void Start()
    {
        myAnimatorList = GetComponentsInChildren<Animator>();
    }

    /// <summary>
    /// Sets the animation accordingly if it is decayed
    /// </summary>
    private void Update()
    {
        if (!isDecayed)
        {
            if (decayScript.platformLifespan == 1)
            {
                isDecayed = true;
                foreach (Animator anim in myAnimatorList)
                {
                    anim.SetBool("isDecayed", true);
                }
            }
        }
    }
    #endregion
    #region Trigger Events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks for if the player has jumped on top of it and it is not currently bouncing
        if (collision.gameObject.CompareTag("Player") && !bounced)
        {
            //Bounces the player
            bounced = true;
            Jump playerJump = collision.gameObject.GetComponent<Jump>();

            //removed to get bounce code out of the jump code
            //playerJump.isBouncing = true; 

            //Plays the animation for the spring flower and changes the sprites depending on if the flower is decayed
            if (isDecayed)
            {
                foreach (Animator anim in myAnimatorList)
                {
                    anim.Play("bounceDEAD");
                }
            }
            else
            {
                foreach (Animator anim in myAnimatorList)
                {
                    anim.Play("bounce");
                }
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //When the player is no longer touching the trigger the bounce is reset for the player to do it again
        if (collision.gameObject.CompareTag("Player"))
        {
            bounced = false;

        }
    }
    #endregion
}
