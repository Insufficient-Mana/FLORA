using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFlower : MonoBehaviour
{
    public bool bounced;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&&!bounced)
        {
            bounced = true;
            Jump playerJump = collision.gameObject.GetComponent<Jump>();
            playerJump.isBouncing = true;
            
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
