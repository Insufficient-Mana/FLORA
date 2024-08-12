using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFlower : MonoBehaviour
{
    public bool bounced;
    public Animator[] myAnimatorList;

    public PlatformDecay decayScript;
    bool isDecayed = false;

    private void Awake()
    {
        decayScript = GetComponent<PlatformDecay>();
    }

    private void Start()
    {
        myAnimatorList = GetComponentsInChildren<Animator>();
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !bounced)
        {
            bounced = true;
            Jump playerJump = collision.gameObject.GetComponent<Jump>();
            playerJump.isBouncing = true;
            
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
        if (collision.gameObject.CompareTag("Player"))
        {
            bounced = false;

        }
    }
}
