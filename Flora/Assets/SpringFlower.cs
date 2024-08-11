using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFlower : MonoBehaviour
{
    public bool bounced;
    public Animator[] myAnimatorList;

    private void Start()
    {
        myAnimatorList = GetComponentsInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&&!bounced)
        {
            bounced = true;
            Jump playerJump = collision.gameObject.GetComponent<Jump>();
            playerJump.isBouncing = true;
            
            foreach(Animator anim in myAnimatorList)
            {
                anim.Play("bounce");
            }

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bounced = false;

        }
    }
}
