using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBonk : MonoBehaviour
{
    public Jump playerJump;

    #region Trigger Events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ends the player's jump if the player hits their head
        if(collision.gameObject.layer == 7)
        {
            playerJump.EndJump();
        }
    }
    #endregion
}
