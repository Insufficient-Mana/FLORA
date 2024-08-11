using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBonk : MonoBehaviour
{
    public Jump playerJump;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            playerJump.EndJump();
        }
    }
}
