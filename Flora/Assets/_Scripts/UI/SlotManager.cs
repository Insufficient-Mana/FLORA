using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [Header("Current Slot Information")]
    public Slot currentSlot;

    [Header("Player Information")]
    public GameObject player;
    public LRMovement movement;
    public Jump jump;

    #region Start and Update
    private void Start()
    {
        //Find the player information and get the movement states
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<LRMovement>();
        jump = player.GetComponent<Jump>();
    }

    private void Update()
    {
        //Only stores the current slot if the player is stood still
        if (!jump.isOnGround || movement.moving)
        {
            currentSlot = null;
        }
    }
    #endregion
}
